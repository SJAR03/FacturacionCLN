using FacturacionCLN.Data;
using FacturacionCLN.DTO;
using Microsoft.EntityFrameworkCore;

namespace FacturacionCLN.Services
{
    public class ReporteVentasService
    {
        private readonly FacturacionDbContext _context;

        public ReporteVentasService(FacturacionDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ReporteVentasMensualesDTO> GenerarReporteVentasMensuales(int? codigoCliente = null, string nombreCliente = null, int? anio = null, int? mes = null, string producto = null, string sku = null)
        {
            var query = _context.Facturas
        .Include(f => f.Cliente)
        .Include(f => f.DetallesFactura)
        .ThenInclude(d => d.Producto)
        .AsQueryable();

            // Aplicar filtros solo si se proporcionan los valores
            if (codigoCliente.HasValue)
                query = query.Where(f => f.IdCliente == codigoCliente.Value);
            if (!string.IsNullOrEmpty(nombreCliente))
                query = query.Where(f => f.Cliente.Nombre.Contains(nombreCliente));
            if (anio.HasValue)
                query = query.Where(f => f.Fecha.Year == anio.Value);
            if (mes.HasValue)
                query = query.Where(f => f.Fecha.Month == mes.Value);
            if (!string.IsNullOrEmpty(producto))
                query = query.Where(f => f.DetallesFactura.Any(d => d.Producto.Descripcion.Contains(producto)));
            if (!string.IsNullOrEmpty(sku))
                query = query.Where(f => f.DetallesFactura.Any(d => d.Producto.SKU.Contains(sku)));

            // Ordenar por fecha de forma descendente (de la más reciente a la más antigua)
            query = query.OrderByDescending(f => f.Fecha);

            // Generar el reporte
            var reporte = query
                .Select(f => new ReporteVentasMensualesDTO
                {
                    CodigoCliente = f.IdCliente,
                    NombreCliente = f.Cliente.Nombre,
                    Anio = f.Fecha.Year,
                    Mes = f.Fecha.Month,
                    TotalDolares = f.MontoTotalDolar,
                    TotalCordobas = f.MontoTotalCordoba,
                    Productos = f.DetallesFactura.Select(d => new ProductoReporteDTO
                    {
                        NombreProducto = d.Producto.Descripcion,
                        SKU = d.Producto.SKU
                    }).ToList()
                }).ToList();

            return reporte;
        }
    }
}
