namespace FacturacionCLN.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public Cliente Cliente { get; set; }
        public decimal MontoTotal { get; set; }
        public string Moneda { get; set; }

        // Propiedad de navegación
        public ICollection<DetalleFactura> DetallesFactura { get; set; } = new List<DetalleFactura>();
    }
}
