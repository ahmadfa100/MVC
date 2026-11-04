namespace UniversityCafeteria2.Models
{
    public class Product
    {
        public string ProductName { get; set; } = "";
        public string Category { get; set; } = "";
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Priority { get; set; } = "";

        public decimal TotalCost { get; set; }
    }
}