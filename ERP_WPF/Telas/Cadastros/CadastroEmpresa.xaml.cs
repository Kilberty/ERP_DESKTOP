using ERP_WPF.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Runtime.ConstrainedExecution;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
namespace ERP_WPF.Telas.Cadastros
{
    public partial class CadastroEmpresa : Window
    {
        private readonly Context _context;

        public CadastroEmpresa()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            _context = new Context(optionsBuilder.Options);
            Loaded += LoadEmpresa;
        }



        private void Check_Desativar(object sender, EventArgs e)
        {
            if (Desativado.IsChecked == true)
            {
                DesativadoLabel.Visibility = Visibility.Visible;
                DesativadoLabel.Content = $"Empresa desativada em : {DateTime.Now:g}";
            }
            else
            {
                DesativadoLabel.Visibility = Visibility.Collapsed;
            }

        }


        private void Textbox_CEP(object sender, TextChangedEventArgs e)
        {
            Masks Mask = new Masks();
            Mask.CEP(sender, e);
        }

        private void Textbox_Numero(object sender, KeyEventArgs e)
        {
            Masks Mask = new Masks();
            Mask.Number(sender, e);
        }

        private void Textbox_CNPJ(object sender, TextChangedEventArgs e)
        {
            Masks Mask = new Masks();
            Mask.CNPJ(sender, e);
        }

        private void Textbox_Telefone(object sender, TextChangedEventArgs e)
        {
            Masks Mask = new Masks();
            Mask.Telefone(sender, e);

        }

        private void Button_Salvar(object sender, EventArgs e)
        {

            if (type.Text == "0")
            {
                string unidade = Unidade.Text;
                string razao = RazaoSocial.Text;
                string fantasia = NomeFantasia.Text;
                string cep = CEP.Text;
                string cnpj = cnpjForm.Text;
                string inscricao = InscricaoEstadual.Text;
                string tel1 = Telefone1.Text;
                string tel2 = Telefone2.Text;
                string cidade = Cidade.Text;
                string endereco = Endereco.Text;
                string uf = UF.Text;
                int numero = int.Parse(Numero.Text);
                string bairro = Bairro.Text;
                string email = Email.Text;
                bool desativado = Desativado.IsChecked == true;

                var empresa = new Empresas
                {
                    Unidade = unidade,
                    RazaoSocial = razao,
                    NomeFantasia = fantasia,
                    CEP = cep,
                    CNPJ = cnpj,
                    InscricaoEstadual = inscricao,
                    Telefone1 = tel1,
                    Telefone2 = tel2,
                    Cidade = cidade,
                    Endereco = endereco,
                    UF = uf,
                    Numero = numero,
                    Bairro = bairro,
                    Email = email,
                    Desativada = desativado
                };

                try
                {
                    _context.Empresas.Add(empresa);
                    _context.SaveChanges();
                    MessageBox.Show("Empresa Cadastrada com Sucesso!");
                    this.Close();
                    ListagemEmpresa listagem = new ListagemEmpresa();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Não foi possível adicionar a empresa: {ex.Message}");
                }
            }
            else
            {
                int id = int.Parse(type.Text);
                var empresa = _context.Empresas.Find(id);

                if (empresa != null)
                {
                    string unidade = Unidade.Text;
                    string razao = RazaoSocial.Text;
                    string fantasia = NomeFantasia.Text;
                    string cep = CEP.Text;
                    string cnpj = cnpjForm.Text;
                    string inscricao = InscricaoEstadual.Text;
                    string tel1 = Telefone1.Text;
                    string tel2 = Telefone2.Text;
                    string cidade = Cidade.Text;
                    string endereco = Endereco.Text;
                    string uf = UF.Text;
                    int numero = int.Parse(Numero.Text);
                    string bairro = Bairro.Text;
                    string email = Email.Text;
                    bool desativado = Desativado.IsChecked == true;

                    empresa.Unidade = unidade;
                    empresa.RazaoSocial = razao;
                    empresa.NomeFantasia = fantasia;
                    empresa.CEP = cep;
                    empresa.CNPJ = cnpj;
                    empresa.InscricaoEstadual = inscricao;
                    empresa.Telefone1 = tel1;
                    empresa.Telefone2 = tel2;
                    empresa.Cidade = cidade;
                    empresa.Endereco = endereco;
                    empresa.UF = uf;
                    empresa.Numero = numero;
                    empresa.Bairro = bairro;
                    empresa.Email = email;
                    empresa.Desativada = desativado;

                    try
                    {
                        // Obtendo a entrada do banco de dados e verificando mudanças
                        var entry = _context.Entry(empresa);
                        if (entry.State == EntityState.Modified)
                        {
                            _context.SaveChanges();
                            MessageBox.Show("Empresa Atualizada com Sucesso!");
                        }
                        else
                        {
                            MessageBox.Show("Nenhuma alteração foi feita.");
                        }
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Não foi possível atualizar a empresa: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Empresa não encontrada.");
                }
            }

        }

        private void LoadEmpresa(object sender, EventArgs e)
        {

            try
            {
                if (int.TryParse(type.Text, out int id))
                {
                    var empresa = _context.Empresas
                        .Where(e => e.id == id)
                        .Select(e => new
                        {
                            e.Unidade,
                            e.RazaoSocial,
                            e.Cidade,
                            e.Endereco,
                            e.UF,
                            e.Numero,
                            e.Bairro,
                            e.Email,
                            e.Desativada,
                            e.CNPJ,
                            e.Telefone1,
                            e.Telefone2,
                            e.CEP,
                            e.NomeFantasia
                        })
                        .FirstOrDefault();
                
                     if(empresa != null)
                    {
                        Unidade.Text = empresa.Unidade;
                        RazaoSocial.Text = empresa.RazaoSocial;
                        Cidade.Text = empresa.Cidade;
                        Endereco.Text = empresa.Endereco;
                        UF.Text = empresa.UF;
                        Numero.Text = empresa.Numero.ToString();
                        Bairro.Text = empresa.Bairro;
                        Email.Text = empresa.Email;
                        Desativado.IsChecked = empresa.Desativada; // Assumindo que é um CheckBox
                        cnpjForm.Text = empresa.CNPJ;
                        Telefone1.Text = empresa.Telefone1;
                        Telefone2.Text = empresa.Telefone2;
                        CEP.Text = empresa.CEP;
                        NomeFantasia.Text = empresa.NomeFantasia;
                    }

                    else
                    {
                        MessageBox.Show("Empresa não encontrada.");
                    }
                }
                else
                {
                    MessageBox.Show("ID inválido.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível carregar os dados da empresa: {ex.Message}");
            }



        }










    }








}
