using System.Data.SqlClient;

public static class ConectaBanco
{
    public static SqlConnection Conectar(Dbconfig config)
    {
        string connectionString = GetConnectionString(config);

        SqlConnection connection = new SqlConnection(connectionString);

        try
        {
            connection.Open();
            Console.WriteLine("Conexão estabelecida com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao abrir a conexão: {ex.Message}");
            connection.Dispose();
            throw;
        }

        return connection;
    }

    public static string GetConnectionString(Dbconfig config)
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

    public static List<string> GetDatabaseNames(Dbconfig config)
    {
        List<string> databaseNames = new List<string>();
        string connectionString = $"Server={config.Host}\\{config.Instancia},{config.PortaBanco};User Id={config.User};Password={config.Password};TrustServerCertificate=True;";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT name FROM sys.databases", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
#pragma warning disable CS8604 // Possível argumento de referência nula.
                    databaseNames.Add(reader["name"].ToString());
#pragma warning restore CS8604 // Possível argumento de referência nula.
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter os nomes dos bancos de dados: {ex.Message}");
        }

        return databaseNames;
    }
}
