namespace Task8.Models
{
    public class RequestItem
    {
        public int Id { get; set; }
        public int supplyRequestId { get; set; }
        public int productId { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public Product product { get; set; }
    }
}
