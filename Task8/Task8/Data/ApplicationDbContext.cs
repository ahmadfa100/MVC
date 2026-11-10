using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task8.Models;

namespace Task8.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserFromIdentity>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Product> products { get; set; }
        public DbSet<SupplyRequest> supllyRequests { get; set; }
        public DbSet<RequestItem> requestItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SupplyRequest>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<RequestItem>()
                .HasOne(i => i.product)
                .WithMany()
                .HasForeignKey(i => i.productId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }

}