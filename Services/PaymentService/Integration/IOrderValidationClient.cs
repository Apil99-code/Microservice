namespace PaymentServices.Intregation
{
    public interface IOrderValidationClient
    {
        public Task<bool> OrderExistsAsync(int orderId, CancellationToken cancellationToken = default);
    }

    public interface IOrerValaditionClient : IOrderValidationClient
    {
    }
}