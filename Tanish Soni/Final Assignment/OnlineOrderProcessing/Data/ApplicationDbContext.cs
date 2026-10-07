using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineOrderProcessing.Models;

namespace OnlineOrderProcessing.Data
{
    public class ApplicationDbContext :  IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }


        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderEvent> OrderEvents { get; set; }

        public DbSet<OrderItems> OrderItems { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.RowVersion)
                .IsRowVersion();

                entity.HasIndex(e => e.SKU)
                .IsUnique();

                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

                entity.Property(e=> e.Price)
                .IsRequired()
                .HasPrecision(10, 2);

                entity.Property(e => e.Stock)
                .IsRequired();

                entity.ToTable(t => t.HasCheckConstraint("CK_Products_Stock_NonNegative", "[Stock] >= 0"));



                entity.HasData(

                    new Product
                    {
                        Id = Guid.Parse("8f4c7d2a-91e5-4b6a-a123-7c9d2e8f4510"),
                        SKU="SKU-pho11",
                        Name="I Phone 18",
                        Price=40000,
                        Stock= 12


                    },
                    new Product
                    {
                        Id = Guid.Parse("8f4c7d2a-91e5-4b6a-a124-7c9d2e8f4510"),
                        SKU = "SKU-shi121",
                        Name = "Shirt",
                        Price = 200,
                        Stock = 22


                    },
                    new Product
                    {
                        Id = Guid.Parse("8f4c7d2a-91e5-4b6a-a125-7c9d2e8f4510"),
                        SKU = "SKU-bag2",
                        Name = "Bag",
                        Price = 150,
                        Stock = 3


                    },
                    new Product
                    {
                        Id = Guid.Parse("8f4c7d2a-91e5-4b6a-a126-7c9d2e8f4510"),
                        SKU = "SKU-shop",
                        Name = "Shop",
                        Price = 20,
                        Stock = 32


                    },
                    new Product
                    {
                        Id = Guid.Parse("8f4c7d2a-91e5-4b6a-a127-7c9d2e8f4510"),
                        SKU = "SKU-fan1",
                        Name = "Fan",
                        Price = 100,
                        Stock = 11


                    },
                    new Product
                    {
                        Id = Guid.Parse("8f4c7d2a-91e5-4b6a-a128-7c9d2e8f4510"),
                        SKU = "SKU-game",
                        Name = "Game",
                        Price = 21,
                        Stock = 2


                    },
                    new Product
                    {
                        Id = Guid.Parse("8f4c7d2a-91e5-4b6a-a129-7c9d2e8f4510"),
                        SKU = "SKU-pen",
                        Name = "Pen",
                        Price = 10,
                        Stock = 120


                    }

                    );

            });

            builder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.OrderRequestKey).IsUnique();

                entity.Property(e => e.CreatedAt)
                .IsRequired();

                entity.Property(e => e.OrderRequestKey).HasMaxLength(100).IsRequired();

                entity.Property(e => e.TotalAmount)
               .IsRequired()
               .HasPrecision(10, 2);

                entity.HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<OrderEvent>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.EventType)
                .IsRequired();

               

                entity.Property(e => e.CreatedAt)
                .IsRequired();

                entity.HasOne(e => e.Order)
                .WithMany(e => e.OrderEvents)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Result)
                .IsRequired();
              
                entity.Property(e => e.CreatedAt)
                .IsRequired();


                entity.Property(e => e.Attempt)
                .IsRequired();

                entity.Property(e => e.CreatedAt)
                .IsRequired();

                entity.HasOne(e => e.Order)
                .WithMany(e => e.Payments)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


            });

            builder.Entity<OrderItems>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity)
                .IsRequired();

                entity.Property(e => e.UnitPrice)
                .IsRequired()
                .HasPrecision(10, 2);

                entity.HasOne(e => e.Order)
                .WithMany(e => e.OrderItems)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


                entity.HasOne(e => e.Product)
                .WithMany(e => e.OrderItems)
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


            });

            builder.Entity<CartItem>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.UserId)
                    .IsRequired();

                e.Property(x => x.ProductId)
                    .IsRequired();

                e.Property(x => x.Quantity)
                    .IsRequired();

                e.HasOne(x => x.Product)
                    .WithMany()
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasIndex(x => new { x.UserId, x.ProductId })
                    .IsUnique();
            });
        }

    }
}
