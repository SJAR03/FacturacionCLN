namespace FacturacionCLN.DTO
{
    public class ReporteVentasMensualesDTO
    {
        public int CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public decimal TotalDolares { get; set; }
        public decimal TotalCordobas { get; set; }
        public List<ProductoReporteDTO> Productos { get; set; }
    }

    public class ProductoReporteDTO
    {
        public string NombreProducto { get; set; }
        public string SKU { get; set; }
        public int CantidadTotal { get; set; }
        public decimal PrecioUnitarioCordoba { get; set; }
        public decimal PrecioUnitarioDolar { get; set; }
    }

}
