namespace UniversityCafeteria2.Models
{
    public class ConfirmedRequest
    {
        public int RequestId { get; set; }

        public List<Product> Products { get; set; } = new();

        public int ProductCount { get; set; }

        public string Priority { get; set; } = "Normal";

        public decimal Subtotal { get; set; }

        public decimal PreparationFee { get; set; }

        public decimal Discount { get; set; }

        public decimal FinalTotal { get; set; }

        public string Status { get; set; } = "Confirmed";
    }
}