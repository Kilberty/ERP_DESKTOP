using System.Windows;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ERP_WPF.Telas
{
    public partial class Seguranca : Window
    {
        public Seguranca()
        {
            InitializeComponent();
            Loaded += Onload;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string hostname = Hostname.Text;
            string instancia = Instancia.Text;
            string user = User.Text;
            string pwd = Pwd.Password;
            string porta = Port.Text;
            string bancoDados = Banco.Text;

            var data = new Dbconfig
            {
                Host = hostname,
                Instancia = instancia,
                User = user,
                Password = pwd,
                PortaBanco = porta,
                BancoNome = bancoDados
            };

            ConfigManager.SaveConfig(data);
            MessageBox.Show("Configuração salva com sucesso.");
        }

        private void Testar_Click(object sender, RoutedEventArgs e)
        {
            string hostname = Hostname.Text;
            string instancia = Instancia.Text;
            string user = User.Text;
            string pwd = Pwd.Password;
            string porta = Port.Text;

            var config = new Dbconfig
            {
                Host = hostname,
                Instancia = instancia,
                User = user,
                Password = pwd,
                PortaBanco = porta
            };

            List<string> databaseNames = ConectaBanco.GetDatabaseNames(config);

            Banco.Items.Clear();
            foreach (string dbName in databaseNames)
            {
                Banco.Items.Add(dbName);
            }
        }

        private void Onload(object sender, RoutedEventArgs e)
        {

            Dbconfig config = ConfigManager.LoadConfig();
            Hostname.Text = config.Host;
            Instancia.Text = config.Instancia;
            Port.Text = config.PortaBanco;
            User.Text = config.User;
            Pwd.Password = config.Password;
            Banco.SelectedItem = config.BancoNome;




        }











    }
}
