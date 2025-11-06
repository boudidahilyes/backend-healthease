using Microsoft.EntityFrameworkCore;
using Prescription.Models;
namespace Prescription.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Prescription.Models.Prescription> Prescriptions { get; set; }
        public DbSet<Medicine> Medicines { get; set; }

    }
}
