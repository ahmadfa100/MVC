namespace codeFirst_DbFirst.CodeFirstModels
{
    public class PurchaseOrder
    {
        public int Id { get; set; }

        public DateOnly OrderDate { get; set; }

        public string Status { get; set; } = null!;

        public int SupplierId { get; set; }

        public  ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();

        public  Supplier Supplier { get; set; } = null!;
    }
}
