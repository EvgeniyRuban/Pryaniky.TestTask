using Microsoft.EntityFrameworkCore;
using Pryaniky.TestTask.Domain.Entities;

namespace Pryaniky.TestTask.DAL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}