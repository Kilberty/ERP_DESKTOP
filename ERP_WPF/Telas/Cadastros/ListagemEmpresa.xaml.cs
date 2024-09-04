using ERP_WPF.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace ERP_WPF.Telas.Cadastros
{
    public partial class ListagemEmpresa : UserControl
    {
        private readonly Context _context;
        private bool isProcessingDoubleClick = false;
        public ListagemEmpresa()
        {
            InitializeComponent();
            Loaded += BuscaEmpresa;
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            _context = new Context(optionsBuilder.Options);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            CadastroEmpresa empresa = new CadastroEmpresa();
            empresa.type.Text = "0";
            empresa.Show();
        }
        public void BuscaEmpresa(object sender, RoutedEventArgs e)
        {
            try
            {
                var empresas = _context.Empresas.ToList();
                dataGridEmpresas.ItemsSource = empresas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível buscar as empresas: {ex.Message}");
            }
        }
        private void AbrirCadastroEmpresa(object sender, MouseButtonEventArgs e)
        {
            if (isProcessingDoubleClick)
                return;

            isProcessingDoubleClick = true;

            try
            {
                if (dataGridEmpresas.SelectedItem is Empresas selectedEmpresa)
                {
                    CadastroEmpresa empresa = new CadastroEmpresa();
                    int id = selectedEmpresa.id;
                    empresa.type.Text = id.ToString();
                    empresa.Show();
                }
                else
                {
                    MessageBox.Show("Nenhuma empresa selecionada.");
                }

                e.Handled = true;
            }
            finally
            {
                isProcessingDoubleClick = false;
            }
        }


    }





}

 






    
    











