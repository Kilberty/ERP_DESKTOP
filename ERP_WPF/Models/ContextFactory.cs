using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace ERP_WPF.Models
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();

            // Carregar a configuração do banco de dados
            Dbconfig config = ConfigManager.LoadConfig();

            // Gerar a string de conexão
            string connectionString = GetConnectionString(config);

            // Configurar o DbContext com a string de conexão
            optionsBuilder.UseSqlServer(connectionString);

            return new Context(optionsBuilder.Options);
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
