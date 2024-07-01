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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = "Action", DisplayOrder=1},
                new Category { Id = 2, CategoryName = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, CategoryName = "History", DisplayOrder = 3 }
                );  
        }
    }
}
