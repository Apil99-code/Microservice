namespace E_commerce.Integrations.Users
{
    public interface IUserValidationClient
    {
        Task<bool> UserExistsAsync(int userId, CancellationToken cancellationToken = default);
    }
}