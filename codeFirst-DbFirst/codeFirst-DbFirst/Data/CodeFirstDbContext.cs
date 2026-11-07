using Microsoft.EntityFrameworkCore;
using codeFirst_DbFirst.Models;
namespace codeFirst_DbFirst.Data
{
    public class CodeFirstDbContext :DbContext
    {
        public CodeFirstDbContext(DbContextOptions<CodeFirstDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<PurchaseOrderItem>().HasOne(x => x.Product)
                .WithMany(x => x.PurchaseOrderItems)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            mb.Entity<Product>()
            .Property(x => x.Price)
            .HasPrecision(10, 2);

            mb.Entity<PurchaseOrderItem>()
                .Property(x => x.UnitPrice)
                .HasPrecision(10, 2);

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}
