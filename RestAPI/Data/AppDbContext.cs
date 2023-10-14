using Microsoft.EntityFrameworkCore;
using RestAPI.Data.models;

namespace RestAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> categories { get; set; }
        
        public DbSet<Item> items { get; set; }  
    }
}
