namespace FacturacionCLN.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; } // Llave foránea de Cliente
        public Cliente Cliente { get; set; } // Propiedad de navegación de Cliente

        // Totales en Córdobas
        public decimal SubTotalCordoba { get; set; }
        public decimal IVACordoba { get; set; }
        public decimal MontoTotalCordoba { get; set; }

        // Totales en Dólares
        public decimal SubTotalDolar { get; set; }
        public decimal IVADolar { get; set; }
        public decimal MontoTotalDolar { get; set; }

        public string Moneda { get; set; } // Moneda utilizada para la factura

        // Propiedad de navegación
        public ICollection<DetalleFactura> DetallesFactura { get; set; } = new List<DetalleFactura>();
    }
}
