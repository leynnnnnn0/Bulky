using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.DataAccess.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Author = "Speed",
                    Description = "All about Ronaldo",
                    ISBN = "I don't know",
                    Title = "Suiiiii",
                    Price = 100.53,
                    ListPrice = 98.03,
                    Price50 = 95.24,
                    Price100 = 90
                },
                new Product
                {
                    Id = 2,
                    Author = "Jane Doe",
                    Description = "Learn about Messi",
                    ISBN = "Another ISBN",
                    Title = "Messi Magic",
                    Price = 110.75,
                    ListPrice = 105.50,
                    Price50 = 100.00,
                    Price100 = 95.00
                 }

                );
            /*modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = "Action", DisplayOrder=1},
                new Category { Id = 2, CategoryName = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, CategoryName = "History", DisplayOrder = 3 }
                );  */



        }
    }
}
