using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_WPF.Models
{
    [Table("Configs")]
    public class Configs
    {
        [Key]
        public int id { get; set; }

        public string Nome { get; set; }
        public int EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public virtual Empresas EmpresaConfig { get; set; }
    }
}