namespace E_commerce.Dtos.Orders
{
    public class CreateOrderRequestDtos
    {
        public int UserId { get; set; }
        public string? OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
