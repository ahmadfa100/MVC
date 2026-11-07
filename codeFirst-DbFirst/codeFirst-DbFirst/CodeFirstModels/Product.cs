namespace codeFirst_DbFirst.CodeFirstModels
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quanitiy { get; set; }

        public int CategoryId { get; set; }

        public int SupplierId { get; set; }

        public  Category Category { get; set; } = null!;

        public  ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();

        public  Supplier Supplier { get; set; } = null!;
    }
}
