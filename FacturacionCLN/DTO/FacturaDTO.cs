using FacturacionCLN.Models;

namespace FacturacionCLN.DTO
{
    public class FacturaDTO
    {
        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public string? NombreCliente { get; set; }
        public DateTime Fecha { get; set; }

        // Totales en Cordobas
        public decimal SubTotalCordoba { get; set; }
        public decimal IVACordoba { get; set; }
        public decimal MontoTotalCordoba { get; set; }

        // Totales en Dólares
        public decimal SubTotalDolar { get; set; }
        public decimal IVADolar { get; set; }
        public decimal MontoTotalDolar { get; set; }
        public List<DetalleFacturaDTO> DetallesFactura { get; set; }
    }
}
