using ERP_WPF.Models;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
using MySql.Data.MySqlClient;
using System.Globalization;
using ERP_WPF.Relatorios;
using CheckBox = System.Windows.Controls.CheckBox;
using Microsoft.EntityFrameworkCore;



namespace ERP_WPF.Telas.Cadastros
{
    
    public partial class CadastroFuncionario : Window
    {
        private readonly Context _context;

        public CadastroFuncionario()
        {
            InitializeComponent();
            Loaded += BuscaFuncionario;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            CEP.Text = "_________";
            CPF.Text = "______________";
            DataAdmissao.Text = "__________";
            DotNetEnv.Env.Load();
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            _context = new Context(optionsBuilder.Options);
        }

        private void ButtonImprimirPonto(object sender, EventArgs e)
        {
            RelatorioPonto relatorio = new RelatorioPonto();
            relatorio.GeraPonto(datagridPonto);
        }
        
        private void Check_Desativado(object sender, EventArgs e)
        {

            if (Desativado.IsChecked == true)
            {
                DesativadoLabel.Visibility = Visibility.Visible;
                DesativadoLabel.Content = $"Funcionário desativado em : {DateTime.Now:g}";
            }
            else
            {
                DesativadoLabel.Visibility = Visibility.Collapsed;
            }

        }
        private void AjustePonto(object sender, EventArgs e)
        {
            List<Ponto> selectedPontos = new List<Ponto>();

            // Itera sobre os itens do DataGrid
            foreach (Ponto item in datagridPonto.Items)
            {
                // Recupera a linha do item
                var row = datagridPonto.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row == null) continue;

                // Recupera o conteúdo da célula da CheckBox na linha
                var checkbox = datagridPonto.Columns[0].GetCellContent(row) as System.Windows.Controls.CheckBox;
                if (checkbox == null) continue;

                // Verifica se a CheckBox está marcada
                if (checkbox.IsChecked == true)
                {
                    selectedPontos.Add(item);
                }
            }

            MessageBox.Show($"Total Selecionado: {selectedPontos.Count}");

            if (selectedPontos.Count > 0)
            {
                AjustePonto ajuste = new AjustePonto(selectedPontos);
                ajuste.Show();
            }
            else
            {
                MessageBox.Show("Nenhum ponto selecionado.");
            }
        }
        
        private void TextBox_Date(object sender, TextChangedEventArgs e)
        {
            Masks Mask = new Masks();
            Mask.Data(sender, e);
        }

        private void TextBox_CPF(object sender, TextChangedEventArgs e)
        {
            Masks Mask = new Masks();
            Mask.CPF(sender, e);
        }

        private void TextBox_CEP(object sender, TextChangedEventArgs e)
        {
            Masks Mask = new Masks();
            Mask.CEP(sender, e);
        }

        private void TextBox_Number(object sender, KeyEventArgs e)
        {
            Masks Mask = new Masks();
            Mask.Number(sender, e);
        }

        private List<Empresas> BuscaEmpresas()
        {
            Dbconfig config = ConfigManager.LoadConfig();

            using (SqlConnection connection = ConectaBanco.Conectar(config))
            {
                string query = "Select id,Unidade from dbo.Empresas";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                List<Empresas> empresas = new List<Empresas>();
                while (reader.Read())
                {
                    Empresas empresaSelect = new Empresas
                    {
                        id = reader.GetInt32(0),
                        Unidade = reader.GetString(1)
                    };

                    empresas.Add(empresaSelect);
                }

                return empresas;
            }

        }

        private void ComboBoxEmpresa()
        {
            List<Empresas> empresas = BuscaEmpresas();
            Empresas.ItemsSource = empresas;
            Empresas.DisplayMemberPath = "Unidade";
            Empresas.SelectedValuePath = "id";
         
        }

        private void BuscaFuncionario(object sender, EventArgs e)
        {
            try
            {
                ComboBoxEmpresa(); 
                if (type.Text != "0")
                {

                    if (int.TryParse(type.Text, out int id))
                    {
                        var funcionario = _context.Funcionarios
                            .Where(f => f.id == id)
                            .Select(f => new
                            {
                                f.id,
                                f.Nome,
                                f.Codigo,
                                f.EmpresaId,
                                f.Usuario,
                                f.Senha,
                                f.RG,
                                f.Numero,
                                f.NomeMae,
                                f.NomePai,
                                f.PisPasep,
                                f.SerieCTPS,
                                f.DataNascimento,
                                f.Emissor,
                                f.eSocial,
                                f.Endereco,
                                f.DataAdmissao,
                                f.DataDemissao,
                                f.DataExpedicaoRG,
                                f.CPF,
                                f.Bairro,
                                f.CTPS,
                                f.CEP,
                                f.Cidade
                            })
                            .FirstOrDefault();

                        if (funcionario != null)
                        {
                            ComboBoxEmpresa();

                            Nome.Text = funcionario.Nome;
                            CodigoPonto.Text = funcionario.Codigo;
                            id_func.Text = funcionario.id.ToString();
                            Empresas.SelectedValue = funcionario.EmpresaId;
                            PontoEmpresa.Text = funcionario.EmpresaId.ToString();
                            Usuario.Text = funcionario.Usuario;
                            Senha.Password = funcionario.Senha;
                            CPF.Text = funcionario.CPF;
                            RG.Text = funcionario.RG;
                            OrgaoEmissor.Text = funcionario.Emissor;
                            Cidade.Text = funcionario.Cidade;
                            DataNascimento.Text = funcionario.DataNascimento.HasValue ? funcionario.DataNascimento.Value.ToString("dd/MM/yyyy") : string.Empty;
                            DataAdmissao.Text = funcionario.DataAdmissao.HasValue ? funcionario.DataAdmissao.Value.ToString("dd/MM/yyyy") : string.Empty;
                            DataDemissao.Text = funcionario.DataDemissao.HasValue ? funcionario.DataDemissao.Value.ToString("dd/MM/yyyy") : string.Empty;
                            DataExpedicao.Text = funcionario.DataExpedicaoRG.HasValue ? funcionario.DataExpedicaoRG.Value.ToString("dd/MM/yyyy") : string.Empty;

                            Endereco.Text = funcionario.Endereco;
                            Bairro.Text = funcionario.Bairro;
                            Numero.Text = funcionario.Numero.HasValue ? funcionario.Numero.Value.ToString() : string.Empty;
                            CTPS.Text = funcionario.CTPS;
                            SerieCTPS.Text = funcionario.SerieCTPS;
                            CEP.Text = funcionario.CEP;
                        }
                        else
                        {
                            MessageBox.Show("Funcionário não encontrado.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("ID inválido.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível buscar o funcionário: {ex.Message}");
            }
        }

        private void Salvar(object sender, RoutedEventArgs e)
        {
            try
            {
               
              if (type.Text != "0")
                {
                    var funcionario = _context.Funcionarios.Find(int.Parse(type.Text));
                    if (funcionario != null)
                    {
                        // Atualizando todos os campos
                        funcionario.Usuario = Usuario.Text;
                        funcionario.Senha = Senha.Password;
                        funcionario.Nome = Nome.Text;
                        funcionario.Codigo = CodigoPonto.Text;
                        funcionario.EmpresaId = (int)Empresas.SelectedValue;
                        funcionario.CEP = CEP.Text;
                        funcionario.Endereco = Endereco.Text;
                        funcionario.Bairro = Bairro.Text;
                        funcionario.Numero = string.IsNullOrEmpty(Numero.Text) ? (int?)null : int.Parse(Numero.Text);
                        funcionario.Cidade = Cidade.Text;
                        funcionario.UF = UF.Text;
                        funcionario.Complemento = Complemento.Text;
                        funcionario.CPF = CPF.Text;
                        funcionario.eSocial = eSocial.Text;
                        funcionario.RG = RG.Text;
                        funcionario.Emissor = OrgaoEmissor.Text;
                        funcionario.DataNascimento = DateOnly.TryParse(DataNascimento.Text, out var dataNascimento) ? dataNascimento : (DateOnly?)null;
                        funcionario.DataAdmissao = DateOnly.TryParse(DataAdmissao.Text, out var dataAdmissao) ? dataAdmissao : (DateOnly?)null;
                        funcionario.DataDemissao = DateOnly.TryParse(DataDemissao.Text, out var dataDemissao) ? dataDemissao : (DateOnly?)null;
                        funcionario.DataExpedicaoRG = DateOnly.TryParse(DataExpedicao.Text, out var dataExpedicaoRG) ? dataExpedicaoRG : (DateOnly?)null;
                        funcionario.PisPasep = PisPasep.Text;
                        funcionario.CTPS = CTPS.Text;
                        funcionario.SerieCTPS = SerieCTPS.Text;
                        funcionario.NomePai = NomePai.Text;
                        funcionario.NomeMae = NomeMae.Text;
                        funcionario.UFNasc = UFNasc.Text;

                        // Salva as alterações no contexto
                        _context.SaveChanges();

                        MessageBox.Show("Atualizado com sucesso!");
                    }
                    else
                    {

                    }
              }
              else
               {
                    try
                    {
                        // Criando uma nova instância de Funcionario
                        var funcionario = new Funcionarios
                        {
                            Usuario = Usuario.Text,
                            Senha = Senha.Password,
                            Nome = Nome.Text,
                            Codigo = CodigoPonto.Text,
                            EmpresaId = (int)Empresas.SelectedValue,
                            CEP = CEP.Text,
                            Endereco = Endereco.Text,
                            Bairro = Bairro.Text,
                            Numero = string.IsNullOrEmpty(Numero.Text) ? (int?)null : int.Parse(Numero.Text),
                            Cidade = Cidade.Text,
                            UF = UF.Text,
                            Complemento = Complemento.Text,
                            CPF = CPF.Text,
                            eSocial = eSocial.Text,
                            RG = RG.Text,
                            Emissor = OrgaoEmissor.Text,
                            DataNascimento = DateOnly.TryParse(DataNascimento.Text, out var dataNascimento) ? dataNascimento : (DateOnly?)null,
                            DataAdmissao = DateOnly.TryParse(DataAdmissao.Text, out var dataAdmissao) ? dataAdmissao : (DateOnly?)null,
                            DataDemissao = DateOnly.TryParse(DataDemissao.Text, out var dataDemissao) ? dataDemissao : (DateOnly?)null,
                            DataExpedicaoRG = DateOnly.TryParse(DataExpedicao.Text, out var dataExpedicaoRG) ? dataExpedicaoRG : (DateOnly?)null,
                            PisPasep = PisPasep.Text,
                            CTPS = CTPS.Text,
                            SerieCTPS = SerieCTPS.Text,
                            NomePai = NomePai.Text,
                            NomeMae = NomeMae.Text,
                            UFNasc = UFNasc.Text
                        };

                        // Adicionando o novo Funcionario ao contexto
                        _context.Funcionarios.Add(funcionario);

                        // Salvando as alterações no contexto
                        _context.SaveChanges();

                        MessageBox.Show("Funcionário adicionado com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Não foi possível adicionar o funcionário: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível salvar os dados: {ex.Message}");
            }
        
        
        }



        private void ButtonPonto(object sender, EventArgs e)
        {
        
            string NomeBanco = String.Empty;
            try
            {
               
                    // Obtendo o ID da empresa a partir do controle PontoEmpresa
                    if (int.TryParse(PontoEmpresa.Text, out int empresaId))
                    {
                        // Consultando o nome a partir da tabela Configs
                        var nomeBanco = _context.Configs
                            .Where(c => c.EmpresaId == empresaId)
                            .Select(c => c.Nome)
                            .FirstOrDefault();

                        if (nomeBanco != null)
                        {
                            NomeBanco = nomeBanco;
                        }
                        else
                        {
                            NomeBanco = string.Empty;
                            MessageBox.Show("Nome não encontrado para a empresa informada.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("ID da empresa inválido.");
                    }
            }
            
            
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível obter o nome do banco: {ex.Message}");
            }

            if (!string.IsNullOrEmpty(NomeBanco))
            {
      
                string User = "u945487164_Kilberty";
                string Password = "Kilberty32316943";
                string Port = "3306";
                string Host = "193.203.175.60";
                string Inicio = DataInicio.Text;
                string Fim = DataFim.Text;
            


                DateTime InicioFiltro = DateTime.ParseExact(Inicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime FimFiltro = DateTime.ParseExact(Fim, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                 



                string mysqlConnectionString = $"Server={Host};Database={NomeBanco};User ID={User};Password={Password};Port={Port};";

                using (MySqlConnection mysqlConnection = new MySqlConnection(mysqlConnectionString))
                {
                    mysqlConnection.Open();
                    string query = @"
                    WITH RECURSIVE Dates AS (
                        SELECT @StartDate AS Date
                        UNION ALL
                        SELECT DATE_ADD(Date, INTERVAL 1 DAY)
                        FROM Dates
                        WHERE Date < @EndDate
                    )
                    SELECT d.Date,
                           r.HoraInicio,
                           r.HoraAlmoco,
                           r.HoraRetorno,
                           r.HoraFim
                    FROM Dates d
                    LEFT JOIN RegistroPonto r
                        ON d.Date = r.Data AND r.FuncionarioID = @FuncionarioID
                    ORDER BY d.Date;";

                    MySqlCommand command = new MySqlCommand(query, mysqlConnection);
                    command.Parameters.AddWithValue("@FuncionarioID", int.Parse(id_func.Text));
                    command.Parameters.AddWithValue("@StartDate", InicioFiltro.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@EndDate", FimFiltro.ToString("yyyy-MM-dd"));

                    MySqlDataReader reader = command.ExecuteReader();

                    List<Ponto> pontos = new List<Ponto>();
                    while (reader.Read())
                    {
                        Ponto ponto = new Ponto
                        {
                            Data = reader.GetDateTime(0),
                            HoraInicio = reader.IsDBNull(1) ? (TimeSpan?)null : reader.GetTimeSpan(1),
                            HoraAlmoco = reader.IsDBNull(2) ? (TimeSpan?)null : reader.GetTimeSpan(2),
                            HoraRetorno = reader.IsDBNull(3) ? (TimeSpan?)null : reader.GetTimeSpan(3),
                            HoraFim = reader.IsDBNull(4) ? (TimeSpan?)null : reader.GetTimeSpan(4)
                        };
                        pontos.Add(ponto);
                    }

                    datagridPonto.ItemsSource = pontos;
                
                
                
                }
             }

         }
        private void SelectAllCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SetAllCheckboxes(true);
        }

        private void SelectAllCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SetAllCheckboxes(false);
        }


        private void SetAllCheckboxes(bool isChecked)
        {
            foreach (var item in datagridPonto.Items)
            {
                if (item is Ponto ponto)
                {
                    ponto.IsSelected = isChecked;
                }
            }

            datagridPonto.Items.Refresh();


        }








    }


    }
















