using Newtonsoft.Json;
using System.IO;

public static class ConfigManager
{
    private static readonly string ConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ERP_WPF", "BancoDados.json");

    public static void SaveConfig(Dbconfig config)
    {
        string jsonContent = JsonConvert.SerializeObject(config, Formatting.Indented);
#pragma warning disable CS8604 // Possível argumento de referência nula.
        Directory.CreateDirectory(Path.GetDirectoryName(ConfigPath));
#pragma warning restore CS8604 // Possível argumento de referência nula.
        File.WriteAllText(ConfigPath, jsonContent);
    }

    public static Dbconfig LoadConfig()
    {
        if (!File.Exists(ConfigPath))
        {
            throw new FileNotFoundException("O arquivo de configuração do banco de dados não foi encontrado.");
        }

        string jsonContent = File.ReadAllText(ConfigPath);
#pragma warning disable CS8603 // Possível retorno de referência nula.
        return JsonConvert.DeserializeObject<Dbconfig>(jsonContent);
#pragma warning restore CS8603 // Possível retorno de referência nula.
    }
}
