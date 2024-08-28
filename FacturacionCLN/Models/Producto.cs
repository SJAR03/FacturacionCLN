using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacturacionCLN.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string SKU { get; set; }

        [Required]
        [StringLength(70)]
        [Column(TypeName = "varchar(70)")]
        public string Descripcion { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Precisión y escala para decimal
        public decimal PrecioCordoba { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioDolar { get; set; }

        [Required]
        public bool Activo { get; set; }
    }
}
