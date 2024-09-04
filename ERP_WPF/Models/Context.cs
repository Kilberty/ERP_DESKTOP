using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ERP_WPF.Models
{
    public class Context : DbContext
    {
        public DbSet<Configs> Configs { get; set; }
        public DbSet<Empresas> Empresas { get; set; }
        public DbSet<Funcionarios> Funcionarios { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Se a configuração não foi feita pelo factory, faça aqui.
                Dbconfig config = ConfigManager.LoadConfig();
                string connectionString = GetConnectionString(config);
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Configs>(entity =>
            {
                entity.ToTable("Configs");
                entity.HasKey(e => e.id);

                entity.Property(e => e.Nome).IsRequired();

                entity.HasOne(e => e.EmpresaConfig)
                      .WithMany(e => e.Configs)
                      .HasForeignKey(e => e.EmpresaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Empresas>(entity =>
            {
                entity.ToTable("Empresas");
                entity.HasKey(e => e.id);

                entity.HasMany(e => e.Funcionarios)
                      .WithOne(f => f.Empresa)
                      .HasForeignKey(f => f.EmpresaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Funcionarios>(entity =>
            {
                entity.ToTable("Funcionario");
                entity.HasKey(e => e.id);

                entity.Property(e => e.Nome).IsRequired();
              
            });
        }

        private static string GetConnectionString(Dbconfig config)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = $"{config.Host}\\{config.Instancia},{config.PortaBanco}",
                InitialCatalog = config.BancoNome,
                UserID = config.User,
                Password = config.Password,
                TrustServerCertificate = true
            };

            return builder.ConnectionString;
        }
    }
}
