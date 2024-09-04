using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_WPF.Models
{
    [Table("Funcionario")]
    public class Funcionarios
    {
        [Key]
        public int id { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; }
        [MaxLength(50)]
        public string? Usuario { get; set; }
        [MaxLength(50)]
        public string? Senha { get; set; }
        [MaxLength(10)]
        public string? Codigo { get; set; }
        public int EmpresaId { get; set; }
        public DateOnly? DataAdmissao { get; set; }
        public DateOnly? DataDemissao { get; set; }
        [MaxLength(10)]
        public string? CEP { get; set; }
        [MaxLength(100)]
        public string? Endereco {  get; set; }
        [MaxLength(50)]
        public string? Bairro { get; set; }
        public int? Numero {  get; set; }
        [MaxLength(100)]
        public string? Cidade { get; set; }
        [MaxLength(2)]
        public string? UF { get; set; }
        [MaxLength(50)]
        public string? Complemento { get; set; }
        [MaxLength(14)]
        public string? CPF { get; set; }
        [MaxLength(30)]
        public string? eSocial { get; set; }
        [MaxLength(20)]
        public string? RG {  get; set; }
        [MaxLength(20)]
        public string? Emissor { get; set; }
        [MaxLength(2)]
        public string? UFRG { get; set; }
        public DateOnly? DataExpedicaoRG { get; set; }
        [MaxLength(20)]
        public string? PisPasep {  get; set; }
        [MaxLength(15)]
        public string? CTPS { get; set; }
        [MaxLength(10)]
        public string? SerieCTPS {  get; set; }
        [MaxLength(100)]
        public string? NomePai {  get; set; }
        [MaxLength(100)]
        public string?   NomeMae {  get; set; }
        [MaxLength(2)]
        public string?  UFNasc {  get; set; }
        public DateOnly? DataNascimento { get; set; }

        
        
        [ForeignKey("EmpresaId")]
        public virtual Empresas Empresa { get; set; }
    }
}