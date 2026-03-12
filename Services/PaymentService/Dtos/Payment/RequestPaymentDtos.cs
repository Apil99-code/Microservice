namespace E_commerce.Dtos.Payment
{
    public class RequestPaymentDtos
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }= string.Empty;
        public DateTime PaymentDate { get; set; }= DateTime.Now;
    }   
}  
