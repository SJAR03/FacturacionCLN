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

        // Precios en Córdobas
        public decimal TotalCordoba { get; set; }

        // Precios en Dólares
        public decimal TotalDolar { get; set; }
    }
}
