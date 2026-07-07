using MediFlow.Database;
using Microsoft.EntityFrameworkCore;

namespace MediFlow
{
    public class AppDbContext : DbContext
    {
        public DbSet<PatientData> Patients { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        
    }
}
