using System.Text.Json;

namespace E_commerce.Integrations.Users
{
    public class UserValidationClient : IUserValidationClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public UserValidationClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<bool> UserExistsAsync(int userId, CancellationToken cancellationToken = default)
        {
            var baseUrl = ResolveUserServiceBaseUrl();
            using var response = await _httpClient.GetAsync(new Uri(new Uri(baseUrl), $"api/E_commerceUsers/{userId}"), cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            throw new InvalidOperationException($"User service call failed with status code {(int)response.StatusCode}.");
        }

        private string ResolveUserServiceBaseUrl()
        {
            var fromEnv = Environment.GetEnvironmentVariable("USER_SERVICE_BASE_URL");
            if (!string.IsNullOrWhiteSpace(fromEnv))
            {
                return EnsureTrailingSlash(fromEnv);
            }

            var fromConfig = _configuration["ServiceEndpoints:UserServiceBaseUrl"];
            if (!string.IsNullOrWhiteSpace(fromConfig))
            {
                return EnsureTrailingSlash(fromConfig);
            }

            var configuredDiscoveryPath = _configuration["ServiceEndpoints:DiscoveryFilePath"];
            var discoveryPath = string.IsNullOrWhiteSpace(configuredDiscoveryPath)
                ? Path.Combine(Path.GetTempPath(), "microservices-service-endpoints.json")
                : configuredDiscoveryPath;

            if (File.Exists(discoveryPath))
            {
                using var document = JsonDocument.Parse(File.ReadAllText(discoveryPath));
                if (document.RootElement.TryGetProperty("ECommerce", out var ecommerceElement)
                    && ecommerceElement.TryGetProperty("HttpBaseUrl", out var baseUrlElement))
                {
                    var discovered = baseUrlElement.GetString();
                    if (!string.IsNullOrWhiteSpace(discovered))
                    {
                        return EnsureTrailingSlash(discovered);
                    }
                }
            }

            throw new InvalidOperationException("User service URL not configured. Set USER_SERVICE_BASE_URL, ServiceEndpoints:UserServiceBaseUrl, or start E-Commerce to generate the discovery file.");
        }

        private static string EnsureTrailingSlash(string url) => url.EndsWith("/") ? url : url + "/";
    }
}