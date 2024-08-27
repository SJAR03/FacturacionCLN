namespace FacturacionCLN.Models
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        public int IdFactura { get; set; } // Llave foránea de Factura
        public Factura Factura { get; set; } // Propiedad de navegación de Factura
        public int IdProducto { get; set; } // Llave foránea de Producto
        public Producto Producto { get; set; } // Propiedad de navegación de Producto

        public int Cantidad { get; set; }

        public decimal PrecioUnitarioCordoba { get; set; }
        public decimal PrecioUnitarioDolar { get; set; }
        public decimal SubtotalCordoba { get; set; }
        public decimal SubtotalDolar { get; set; }
    }
}
