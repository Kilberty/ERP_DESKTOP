using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ERP_WPF.Telas.Cadastros.CadastroFuncionario;
using System.Windows.Controls;

namespace ERP_WPF.Relatorios
{
    public class RelatorioPonto
    {

        public void GeraPonto(DataGrid dataGrid)
        {
            string filePath = "Ponto.pdf";

            try
            {
                using (var document = new Document())
                {
                    PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                    document.Open();

                    

                    var table = new PdfPTable(dataGrid.Columns.Count);

                    // Adicionar cabeçalhos
                    foreach (DataGridColumn column in dataGrid.Columns)
                    {
                        table.AddCell(new Phrase(column.Header.ToString()));
                    }

                    // Adicionar dados
                    foreach (var item in dataGrid.Items)
                    {
                        var ponto = item as Ponto;
                        if (ponto != null)
                        {
                            table.AddCell(new Phrase(ponto.Data.ToShortDateString()));
                            table.AddCell(new Phrase(ponto.HoraInicio.HasValue ? ponto.HoraInicio.Value.ToString(@"hh\:mm") : string.Empty));
                            table.AddCell(new Phrase(ponto.HoraAlmoco.HasValue ? ponto.HoraAlmoco.Value.ToString(@"hh\:mm") : string.Empty));
                            table.AddCell(new Phrase(ponto.HoraRetorno.HasValue ? ponto.HoraRetorno.Value.ToString(@"hh\:mm") : string.Empty));
                            table.AddCell(new Phrase(ponto.HoraFim.HasValue ? ponto.HoraFim.Value.ToString(@"hh\:mm") : string.Empty));
                        }
                    }

                    document.Add(table);
                }

                // Abrir o PDF
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                // Tratar exceções
                MessageBox.Show("Erro ao gerar PDF: " + ex.Message);
            }
        }

















    }
}
