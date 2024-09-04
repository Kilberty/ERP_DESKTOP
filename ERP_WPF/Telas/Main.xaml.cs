using System.Windows;

namespace ERP_WPF.Telas
{
    /// <summary>
    /// Lógica interna para Main.xaml
    /// </summary>
    public partial class Main : Window
    {


        public Main()
        {
            InitializeComponent();
#pragma warning disable CS8622 // A nulidade de tipos de referência no tipo de parâmetro não corresponde ao delegado de destino (possivelmente devido a atributos de nulidade).
            this.Closing += new System.ComponentModel.CancelEventHandler(Main_Closing);
#pragma warning restore CS8622 // A nulidade de tipos de referência no tipo de parâmetro não corresponde ao delegado de destino (possivelmente devido a atributos de nulidade).
        }


        private void Main_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Vendas(object sender, EventArgs e)
        {
            Vendas vendas = new Vendas();
            MainContent.Content = vendas;
        }

        private void Button_Cadastros(object sender, EventArgs e)
        {
            CadastrosHidden.Visibility = CadastrosHidden.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void Button_Estoque(object sender, EventArgs e)
        {
            EstoqueHidden.Visibility = EstoqueHidden.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

        }

        private void Button_Pessoas(object sender, EventArgs e)
        {
            PessoasHidden.Visibility = PessoasHidden.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void Button_Funcionarios(object sender, EventArgs e)
        {
            Cadastros.ListagemFuncionario funcionarios = new Cadastros.ListagemFuncionario();
            MainContent.Content = funcionarios;

        }

        private void Button_Config(object sender, EventArgs e)
        {
            ConfigHidden.Visibility = ConfigHidden.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void Button_Empresas(object sender, EventArgs e)
        {
            Cadastros.ListagemEmpresa empresa = new Cadastros.ListagemEmpresa();
            MainContent.Content = empresa;

        }

    }








}
