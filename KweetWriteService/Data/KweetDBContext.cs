using Microsoft.EntityFrameworkCore;
using KweetWriteService.Models;

namespace KweetWriteService.Data
{
    public class KweetDBContext : DbContext
    {
        public KweetDBContext(DbContextOptions<KweetDBContext> options) : base(options) 
        { 
        }

        public DbSet<Kweet> Kweets { get; set; }
    }
}
