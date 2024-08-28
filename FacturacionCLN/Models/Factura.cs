using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacturacionCLN.Models
{
    public class Factura
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public int IdCliente { get; set; } // Llave foránea de Cliente

        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; } // Propiedad de navegación de Cliente

        // Totales en Córdobas
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotalCordoba { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal IVACordoba { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoTotalCordoba { get; set; }

        // Totales en Dólares
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotalDolar { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal IVADolar { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MontoTotalDolar { get; set; }

        // Propiedad de navegación
        public ICollection<DetalleFactura> DetallesFactura { get; set; } = new List<DetalleFactura>();
    }
}
