using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data.Model
{
    public class APIDbContect : DbContext
    {
        public APIDbContect(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Customer>();
            _ = modelBuilder.Entity<Product>();
            _ = modelBuilder.Entity<Cart>();
            _ = modelBuilder.Entity<Order>();
            _ = modelBuilder.Entity<OrderDetail>();

            base.OnModelCreating(modelBuilder);
        }
    }
}