using InstantEatService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstantEatService
{
    public partial class InstantEatDbContext : DbContext
    {
        public InstantEatDbContext()
        {
                
        }
        public InstantEatDbContext(DbContextOptions<InstantEatDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<Cart> Carts { get; set; }
         public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>().HasKey(c => c.Id).HasName("PK_Cart_Id");
            modelBuilder.Entity<Category>().HasKey(c => c.Id).HasName("PK_Category_Id");
            modelBuilder.Entity<Client>().HasKey(c => c.Id).HasName("PK_Client_Id");
            modelBuilder.Entity<FoodItem>().HasKey(c => c.Id).HasName("PK_FoodItem_Id");


            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Carts");

                entity.HasMany(c => c.FoodItems)
                    .WithMany(f => f.Carts)
                    .UsingEntity(j => j.ToTable("Cart/FoodItem"));

                entity.HasOne(c => c.Client)
                    .WithMany(c => c.Carts)
                    .HasForeignKey(c => c.ClientId)
                    .HasConstraintName("FK_Client/Carts");
            });

            modelBuilder.Entity<FoodItem>(entity =>
            {
                entity.ToTable("FoodItems");

                //entity.HasMany(f => f.Categories)
                //    .WithMany(c => c.FoodItems)
                //    .UsingEntity(j => j.ToTable("Category/FoodItem"));
            });

            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Client>().ToTable("Clients");

            OnModelCreatingPartial(modelBuilder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
