using ERP_WPF.Models;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using UserControl = System.Windows.Controls.UserControl;
namespace ERP_WPF.Telas.Cadastros
{
    public partial class ListagemFuncionario : UserControl
    {
        private bool isProcessingDoubleClick = false;

        public ListagemFuncionario()
        {
            InitializeComponent();
            Loaded += BuscaFuncionario;

            // Adiciona o evento de clique duplo ao DataGrid

        }

        private void Button_Novo(object sender, RoutedEventArgs e)
        {
            CadastroFuncionario func = new CadastroFuncionario();
            func.type.Text = "0";
            func.Show();
        }

        private void BuscaFuncionario(object sender, RoutedEventArgs e)
        {
            Dbconfig config = ConfigManager.LoadConfig();

            using (SqlConnection connection = ConectaBanco.Conectar(config))
            {
                string query = "SELECT id, Nome FROM dbo.Funcionario";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<Funcionarios> funcionarios = new List<Funcionarios>();

                while (reader.Read())
                {
                    Funcionarios funcionario = new Funcionarios
                    {
                        id = reader.GetInt32(0),
                        Nome = reader.GetString(1),
                    };
                    funcionarios.Add(funcionario);
                }

                dataGridFuncionario.ItemsSource = funcionarios;
            }
        }

        // Flag para controlar a execução


        private void AbrirCadastro(object sender, MouseButtonEventArgs e)
        {
            // Verifica se já estamos processando um clique duplo
            if (isProcessingDoubleClick)
                return;

            isProcessingDoubleClick = true;

            try
            {
                // Certifica-se de que há um item selecionado e ele é do tipo correto
                if (dataGridFuncionario.SelectedItem is Funcionarios selectedFuncionario)
                {
                    CadastroFuncionario funcionario = new CadastroFuncionario();
                    int id = selectedFuncionario.id;
                    funcionario.type.Text = id.ToString();
                    funcionario.Show();
                }

                // Marca o evento como tratado para evitar duplicação
                e.Handled = true;
            }
            finally
            {
                // Reseta o flag para permitir o próximo clique duplo
                isProcessingDoubleClick = false;
            }
        }

    }
}
