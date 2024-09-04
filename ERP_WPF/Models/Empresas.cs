using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_WPF.Models
{
    [Table("Empresas")]
    public class Empresas
    {
        [Key]
        public int id { get; set; }
        [MaxLength(30)]
        public string Unidade { get; set; }
        [MaxLength(60)]
        public string RazaoSocial { get; set; }
        [MaxLength(60)]
        public string NomeFantasia { get; set; }
        public string? CEP { get; set; }
        [MaxLength(18)]
        public string CNPJ { get; set; }
        [MaxLength(20)]
        public string? InscricaoEstadual { get; set; }
        [MaxLength(20)]
        public string? Telefone1 { get; set; }
        [MaxLength(20)]
        public string? Telefone2 { get; set; }
        [MaxLength(50)]
        public string? Cidade { get; set; }
        [MaxLength(100)]
        public string? Endereco { get; set; }
        [MaxLength(2)]
        public string? UF { get; set; }
        public int? Numero { get; set; }
        [MaxLength(50)]
        public string? Bairro { get; set; }
        [MaxLength(40)]
        public string? Email { get; set; }
        public bool Desativada { get; set; } = false;

        public virtual ICollection<Configs> Configs { get; set; }
        public virtual ICollection<Funcionarios> Funcionarios { get; set; }
    }
}
