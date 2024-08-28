using System.ComponentModel.DataAnnotations;

namespace FacturacionCLN.Models
{
    public class TasaCambio
    {
        public int Id { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "La tasa de cambio debe ser un número positivo mayor a cero.")]
        public decimal Tasa { get; set; }

        [Required]
        public DateTime Fecha { get; set; }
    }
}
