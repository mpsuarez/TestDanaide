using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDanaide.Persistence.Entities;

namespace TestDanaide.Persistence
{
    public class TestDanaideDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }


        public TestDanaideDbContext(DbContextOptions<TestDanaideDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Cart>(entity => {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.User)
                    .WithMany(x => x.Carts)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(x => x.Total)
                    .HasColumnType("decimal(18,2)");

            });

            modelBuilder.Entity<CartProduct>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasIndex(x => new { x.CartId, x.ProductId }).IsUnique();

                entity.HasOne(x => x.Cart)
                    .WithMany(x => x.CartProducts)
                    .HasForeignKey(x => x.CartId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Product)
                    .WithMany(x => x.CartProducts)
                    .HasForeignKey(x => x.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Product>(entity => {

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Price)
                    .HasColumnType("decimal(18,2)");

            });


            modelBuilder.Entity<User>(entity => {

                entity.HasKey(x => x.Id);

            });
                

            base.OnModelCreating(modelBuilder);
        }

    }
}
