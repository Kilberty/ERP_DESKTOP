using System.Windows;
using MessageBox = System.Windows.Forms.MessageBox;
namespace ERP_WPF.Telas
{
    /// <summary>
    /// Lógica interna para Password.xaml
    /// </summary>
    public partial class Password : Window
    {
        public Password()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (PWD.Password == "3512")
            {
                Seguranca seg = new Seguranca();
                seg.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Senha incorreta");
            }

        }
    }
}
