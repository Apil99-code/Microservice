using System.Text.Json;

namespace PaymentServices.Intregation
{
    public class OrderValidationClient : IOrderValidationClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public OrderValidationClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<bool> OrderExistsAsync(int orderId, CancellationToken cancellationToken = default)
        {
            var baseUrl = ResolveOrderServiceBaseUrl();
            using var response = await _httpClient.GetAsync(new Uri(new Uri(baseUrl), $"api/E_commerceOrders/{orderId}"), cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            throw new InvalidOperationException($"Order service call failed with status code {(int)response.StatusCode}.");
        }

        private string ResolveOrderServiceBaseUrl()
        {
            var fromEnv = Environment.GetEnvironmentVariable("ORDER_SERVICE_BASE_URL");
            if (!string.IsNullOrWhiteSpace(fromEnv))
            {
                return EnsureTrailingSlash(fromEnv);
            }

            var fromConfig = _configuration["ServiceEndpoints:OrderServiceBaseUrl"];
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
                if (document.RootElement.TryGetProperty("OrderService", out var orderServiceElement)
                    && orderServiceElement.TryGetProperty("HttpBaseUrl", out var baseUrlElement))
                {
                    var discovered = baseUrlElement.GetString();
                    if (!string.IsNullOrWhiteSpace(discovered))
                    {
                        return EnsureTrailingSlash(discovered);
                    }
                }
            }

            throw new InvalidOperationException("Order service URL not configured. Set ORDER_SERVICE_BASE_URL, ServiceEndpoints:OrderServiceBaseUrl, or provide the discovery file.");
        }

        private static string EnsureTrailingSlash(string url) => url.EndsWith("/") ? url : url + "/";
    }
}