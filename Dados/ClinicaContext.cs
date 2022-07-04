using ClinicaSorrisoEntity.Models;
using Microsoft.EntityFrameworkCore;


namespace ClinicaSorrisoEntity.Dados
{
    public class ClinicaContext : DbContext
    {
        public DbSet<Paciente> ?Pacientes { get; set; }
        public DbSet<Consulta> ?Consultas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = ClinicaEntityDTB; Trusted_Connection = true;")
                          .UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Paciente>()
            .HasOne(paciente => paciente.ConsultaMarcada)
            .WithOne(consulta => consulta.Paciente)
            .HasForeignKey<Consulta>(consulta => consulta.PacienteId);
        }
    }
}
