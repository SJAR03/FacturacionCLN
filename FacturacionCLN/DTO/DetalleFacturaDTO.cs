namespace FacturacionCLN.DTO
{
    public class DetalleFacturaDTO
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitarioCordoba { get; set; }
        public decimal PrecioUnitarioDolar { get; set; }
    }
}
