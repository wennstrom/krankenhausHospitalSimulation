using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using KrankenhausSjukhuset.Models;

namespace KrankenhausSjukhuset
{
    public class KrankenhausSjukhusetDbContext : DbContext
    {
        public KrankenhausSjukhusetDbContext()
        {

        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Status> Satuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //sätter foreign keys            
            modelBuilder.Entity<Patient>()
                        .HasKey(p => p.SSN);

            modelBuilder.Entity<Status>()
                        .HasKey(s => s.ID);

            // hade problem med att setta foreign keyn på status id 
            modelBuilder.Entity<Status>()
                        .Property(s => s.ID)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None); 
        }

    }
}
