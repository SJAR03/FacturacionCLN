using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacturacionCLN.Models
{
    public class DetalleFactura
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdFactura { get; set; } // Llave foránea de Factura

        [ForeignKey("IdFactura")]
        public Factura Factura { get; set; } // Propiedad de navegación de Factura

        [Required]
        public int IdProducto { get; set; } // Llave foránea de Producto

        [ForeignKey("IdProducto")]
        public Producto Producto { get; set; } // Propiedad de navegación de Producto

        [Required]
        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitarioCordoba { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitarioDolar { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubtotalCordoba { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubtotalDolar { get; set; }
    }
}
