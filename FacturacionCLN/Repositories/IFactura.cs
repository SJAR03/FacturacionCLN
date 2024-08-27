using FacturacionCLN.DTO;

namespace FacturacionCLN.Repositories
{
    public interface IFactura
    {
        FacturaDTO CrearFactura(FacturaDTO facturaDTO);
        IEnumerable<FacturaDTO> ListarFacturas();
        IEnumerable<FacturaDTO> ObtenerFacturaPorIdOCliente(string criterio);
        bool EliminarFactura(int id);
    }
}
