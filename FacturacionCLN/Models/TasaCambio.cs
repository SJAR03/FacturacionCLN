using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacturacionCLN.Models
{
    public class TasaCambio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "La tasa de cambio debe ser un número positivo mayor a cero.")]
        [Column(TypeName = "decimal(18,6)")]
        public decimal Tasa { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }
    }
}
