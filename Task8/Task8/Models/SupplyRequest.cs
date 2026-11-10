namespace Task8.Models
{
    public enum StatusValues
    {
        Pending,
        Approved,
        Rejected
    }

    public class SupplyRequest
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public decimal TotalPrice { get; set; }

        public StatusValues Status { get; set; } = StatusValues.Pending;

        public string UserId { get; set; } = string.Empty;
        public UserFromIdentity User { get; set; } = null!;
        public ICollection<RequestItem> Items { get; set; } 

    }
}