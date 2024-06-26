using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data
{
    public class ApplicationDBContext(DbContextOptions dbContextOptions) : DbContext( dbContextOptions)
    {
        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Comment> Comments { get; set; }
        
    }
}