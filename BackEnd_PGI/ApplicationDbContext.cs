using BackEnd_PGI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace BackEnd_PGI
{

    public class ApplicationDbContext : DbContext
    {
        private string? _connectionString = "Server=localhost;Port=5432;Database=db_PGI;User Id=postgres;Password=postgres;";

        public ApplicationDbContext()
        {
            _connectionString = GetDefaultConnectionString();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }
        public ApplicationDbContext(ConfigurationManager configurationManager)
        {
            _connectionString = configurationManager.GetSection("ConnectionString").Value;

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public ApplicationDbContext(string? connectionString)
        {
            _connectionString = string.IsNullOrEmpty(connectionString) ? GetDefaultConnectionString() : connectionString;

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DbSet<Caso> Casos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<IOC> IOCs { get; set; }
        public DbSet<TipoIOC> TipoIOCs { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Analisis> Analisis { get; set; }
        public DbSet<Analista> Analistas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<TipoAsset> TipoAssets { get; set; }
        public DbSet<Maquina> Maquinas { get; set; }

        private string? GetDefaultConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("dbsettings.json")
                    .AddEnvironmentVariables()
                    .Build();

                _connectionString = configuration.GetSection("ConnectionString").Value;
            }
            return _connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_connectionString != null)
            {  //optionsBuilder.UseSqlServer(_connectionString);
                optionsBuilder.UseNpgsql(_connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Caso>()
                .HasMany(s => s.Tareas);

            modelBuilder.Entity<Caso>()
                .HasMany(s => s.Analisiss);

            modelBuilder.Entity<Caso>()
                .HasMany(s => s.Assets);

            modelBuilder.Entity<Caso>()
                .HasMany(s => s.Maquinas);

            modelBuilder.Entity<Rol>()
            .HasMany(s => s.Usuarios);

            modelBuilder.Entity<Asset>()
            .HasMany(s => s.IOCs);

            modelBuilder.Entity<Asset>()
            .HasOne(s => s.TipoAsset);

            modelBuilder.Entity<IOC>()
            .HasOne(s => s.TipoIOC);

            modelBuilder.Entity<Caso>()
                .Property(caso => caso.Estado)
                .HasConversion(
                    v => v.ToString(), // De Enum a string
                    v => (TipoEstadoCaso)Enum.Parse(typeof(TipoEstadoCaso), v)); // De string a Enum

            modelBuilder.Entity<Tarea>()
                .Property(Tarea => Tarea.Estado)
                .HasConversion(
                    v => v.ToString(), // De Enum a string
                    v => (TipoEstadoTarea)Enum.Parse(typeof(TipoEstadoTarea), v!)); // De string a Enum

            modelBuilder.Entity<TipoIOC>()
                .Property(TipoIOC => TipoIOC.BuscarEn)
                .HasConversion(
                    v => v.ToString(), // De Enum a string
                    v => (BuscarEn)Enum.Parse(typeof(BuscarEn), v!)); // De string a Enum

        }

    }
}
