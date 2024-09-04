using System.Windows.Controls;
using System.Windows.Input;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using TextBox = System.Windows.Controls.TextBox;

namespace ERP_WPF.Telas.Cadastros
{
    class Masks
    {

        public void Data(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.MaxLength = 10;
                textBox.TextChanged -= Data; // Desabilita temporariamente o evento para evitar loops


                // Remove todos os caracteres não numéricos
                string date = new string(textBox.Text.Where(char.IsDigit).ToArray());

                // Insere as barras na posição correta
                if (date.Length > 2 && date[2] != '/')
                {
                    date = date.Insert(2, "/");
                }
                if (date.Length > 5 && date[5] != '/')
                {
                    date = date.Insert(5, "/");
                }

                // Atualiza o texto do TextBox
                textBox.Text = date;

                // Reposiciona o cursor corretamente
                textBox.CaretIndex = date.Length;

                textBox.TextChanged += Data; // Re-habilita o evento
            }
        }

        public void CPF(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.MaxLength = 14;
                textBox.TextChanged -= CPF;

                string cpf = new string(textBox.Text.Where(char.IsDigit).ToArray());


                if (cpf.Length > 3)
                {
                    cpf = cpf.Insert(3, ".");
                }
                if (cpf.Length > 7)
                {
                    cpf = cpf.Insert(7, ".");
                }
                if (cpf.Length > 11)
                {
                    cpf = cpf.Insert(11, "-");
                }

                textBox.Text = cpf;

                textBox.CaretIndex = cpf.Length;

                textBox.TextChanged += CPF;
            }
        }

        public void CEP(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.MaxLength = 9;
                textBox.TextChanged -= CEP; // Desabilita temporariamente o evento para evitar loops

                // Remove todos os caracteres não numéricos
                string cep = new string(textBox.Text.Where(char.IsDigit).ToArray());

                // Insere o hífen na posição correta
                if (cep.Length > 5)
                {
                    cep = cep.Insert(5, "-");
                }

                // Atualiza o texto do TextBox
                textBox.Text = cep;

                // Reposiciona o cursor corretamente
                textBox.CaretIndex = textBox.Text.Length;

                textBox.TextChanged += CEP; // Re-habilita o evento
            }
        }


        public void CNPJ(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.MaxLength = 18; // O comprimento máximo de um CNPJ formatado é 18 caracteres
                textBox.TextChanged -= CNPJ; // Desabilita temporariamente o evento para evitar loops

                // Remove todos os caracteres não numéricos
                string cnpj = new string(textBox.Text.Where(char.IsDigit).ToArray());

                // Insere os pontos, a barra e o traço na posição correta
                if (cnpj.Length > 2)
                {
                    cnpj = cnpj.Insert(2, ".");
                }
                if (cnpj.Length > 6)
                {
                    cnpj = cnpj.Insert(6, ".");
                }
                if (cnpj.Length > 10)
                {
                    cnpj = cnpj.Insert(10, "/");
                }
                if (cnpj.Length > 15)
                {
                    cnpj = cnpj.Insert(15, "-");
                }

                // Atualiza o texto do TextBox
                textBox.Text = cnpj;

                // Reposiciona o cursor corretamente
                textBox.CaretIndex = cnpj.Length;

                textBox.TextChanged += CNPJ; // Re-habilita o evento
            }
        }
        public void Telefone(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.MaxLength = 14;
                textBox.TextChanged -= Telefone;
                string Tel = new string(textBox.Text.Where(char.IsDigit).ToArray());

                if (Tel.Length > 1)
                {
                    Tel = Tel.Insert(0, "(");
                }
                if (Tel.Length > 4)
                {
                    Tel = Tel.Insert(3, ")");
                    Tel = Tel.Insert(4, " ");
                }
                if (Tel.Length > 4 && Tel[5] == '3')
                {
                    if (Tel.Length > 10)
                    {
                        Tel = Tel.Insert(9, "-");
                    }
                }
                if (Tel.Length > 4 && Tel[5] != '3')
                {
                    textBox.MaxLength = 15;

                    if (Tel.Length > 11)
                    {
                        Tel = Tel.Insert(10, "-");
                        if (Tel[5] != '9')
                        {
                            Tel = Tel.Insert(5, "9");
                        }
                    }
                }





                textBox.Text = Tel;
                textBox.CaretIndex = Tel.Length;
                textBox.TextChanged += Telefone;


            }
        }














        public void Number(object sender, KeyEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Permite a navegação usando as setas, backspace e delete
                if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Back || e.Key == Key.Delete || e.Key == Key.Tab)
                {
                    return;
                }

                // Evita a entrada de qualquer coisa que não seja um número
                if (!char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)))
                {
                    e.Handled = true;
                }
            }
        }
















    }


}
