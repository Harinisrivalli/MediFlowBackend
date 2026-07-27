using MediFlow.Database;
using MediFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace MediFlow
{
    public class AppDbContext : DbContext
    {
        public DbSet<PatientData> Patients { get; set; }
        public DbSet<CreateDoctor> doctors { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        
    }
}
