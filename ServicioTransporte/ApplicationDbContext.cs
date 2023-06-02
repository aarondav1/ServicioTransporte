using Microsoft.EntityFrameworkCore;
using ServicioTransporte.Entities;

namespace ServicioTransporte
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<AsignacionRutaParada>().HasKey(arp => new { arp.Id_Ruta, arp.Id_Parada });
        //}

        //TABLAS
        public DbSet<AsignacionBusConductor> AsignacionesBusConductor { get; set; }
        public DbSet<AsignacionRutaBus> AsignacionesRutaBus { get; set; }
        public DbSet<AsignacionRutaParada> AsignacionesRutaParada { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Conductor> Conductores { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Parada> Paradas { get; set; }
        public DbSet<Ruta> Rutas { get; set; }
        public DbSet<TipoLicencia> TiposLicencia { get; set; }
        
    }
}
