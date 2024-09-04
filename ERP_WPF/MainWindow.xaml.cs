using System.Data.SqlClient;
using System.Windows;

namespace ERP_WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Loaded += App_Loaded;
        }

        private void btnClickMe_Click(object sender, RoutedEventArgs e)
        {
            string username = User.Text;
            string password = Pwd.Password;

            if (AuthenticateUser(username, password))
            {
                Telas.Main mainWindow = new Telas.Main();
                mainWindow.Show();
                this.Hide();
            }
            else
            {

            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            Dbconfig config = ConfigManager.LoadConfig();

            using (SqlConnection connection = ConectaBanco.Conectar(config))
            {
                // Consulta SQL para verificar se o usuário e senha correspondem a um registro na tabela de usuários
                string query = "SELECT COUNT(*) FROM dbo.Funcionario WHERE Usuario = @Usuario AND Senha = @Senha";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Usuario", username);
                command.Parameters.AddWithValue("@Senha", password);

                int count = (int)command.ExecuteScalar(); // Retorna o número de registros correspondentes

                return count > 0; // Se count for maior que 0, o usuário foi autenticado com sucesso
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Telas.Password pass = new Telas.Password();
            pass.Show();
        }

        private void App_Loaded(object sender, RoutedEventArgs e)
        {
            User.Focus();
        }




    }
}
