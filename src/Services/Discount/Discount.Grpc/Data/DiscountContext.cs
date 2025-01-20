using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;
    public DiscountContext(DbContextOptions options) : base(options)
    {
    }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ** OnModelCreating ile öncelikle initial data (başlangıç verisi) eklememize yarar.
        // ** Veritabanı oluşturulduğunda otomatik olarak elle veritabanına veri eklememizi sağlar.

        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
            new Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100 }
            );
    }
}
