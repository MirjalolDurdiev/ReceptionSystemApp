using Microsoft.EntityFrameworkCore;
using ReceptionSystemApp.Models;

namespace ReceptionSystemApp.Data
{
    public class ReceptionDbContext:DbContext
    {
        public ReceptionDbContext(DbContextOptions<ReceptionDbContext> options) : base(options) { }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Reception> Receptions { get; set; }
    }
}
