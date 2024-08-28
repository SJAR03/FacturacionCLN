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

        public IEnumerable<ReporteVentasMensualesDTO> GenerarReporteVentasMensuales(
            int? codigoCliente = null,
            string nombreCliente = null,
            int? anio = null,
            int? mes = null,
            string producto = null,
            string sku = null)
        {
            var query = _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.DetallesFactura)
                .ThenInclude(d => d.Producto)
                .AsQueryable();

            // Aplicar filtros solo si se proporcionan los valores
            if (codigoCliente.HasValue)
                query = query.Where(f => f.Cliente.Codigo == codigoCliente.Value.ToString());
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

            // Primero se obtiene la lista de productos por cliente, año y mes
            var productosPorFactura = query.SelectMany(f => f.DetallesFactura)
                .Where(d => (string.IsNullOrEmpty(sku) || d.Producto.SKU == sku) &&
                            (string.IsNullOrEmpty(producto) || d.Producto.Descripcion.Contains(producto)))
                .Select(d => new
                {
                    CodigoCliente = d.Factura.IdCliente,
                    NombreCliente = d.Factura.Cliente.Nombre,
                    Anio = d.Factura.Fecha.Year,
                    Mes = d.Factura.Fecha.Month,
                    NombreProducto = d.Producto.Descripcion,
                    SKU = d.Producto.SKU,
                    Cantidad = d.Cantidad,
                    SubtotalCordoba = d.Cantidad * d.PrecioUnitarioCordoba,
                    SubtotalDolar = d.Cantidad * d.PrecioUnitarioDolar,
                    PrecioUnitarioCordoba = d.PrecioUnitarioCordoba,
                    PrecioUnitarioDolar = d.PrecioUnitarioDolar
                }).ToList();

            // Luego se agrupa por cliente, año, mes, SKU, NombreProducto, y PrecioUnitario
            var reporte = productosPorFactura
                .GroupBy(p => new
                {
                    p.CodigoCliente,
                    p.NombreCliente,
                    p.Anio,
                    p.Mes,
                    p.SKU,
                    p.NombreProducto,
                    p.PrecioUnitarioCordoba,
                    p.PrecioUnitarioDolar
                })
                .Select(g => new
                {
                    g.Key.CodigoCliente,
                    g.Key.NombreCliente,
                    g.Key.Anio,
                    g.Key.Mes,
                    TotalDolares = g.Sum(p => p.SubtotalDolar),
                    TotalCordobas = g.Sum(p => p.SubtotalCordoba),
                    Productos = new ProductoReporteDTO
                    {
                        NombreProducto = g.Key.NombreProducto,
                        SKU = g.Key.SKU,
                        CantidadTotal = g.Sum(p => p.Cantidad),
                        PrecioUnitarioCordoba = g.Key.PrecioUnitarioCordoba,
                        PrecioUnitarioDolar = g.Key.PrecioUnitarioDolar
                    }
                })
                .GroupBy(r => new { r.CodigoCliente, r.NombreCliente, r.Anio, r.Mes })
                .Select(g => new ReporteVentasMensualesDTO
                {
                    CodigoCliente = g.Key.CodigoCliente,
                    NombreCliente = g.Key.NombreCliente,
                    Anio = g.Key.Anio,
                    Mes = g.Key.Mes,
                    TotalDolares = g.Sum(r => r.TotalDolares),
                    TotalCordobas = g.Sum(r => r.TotalCordobas),
                    Productos = g.Select(r => r.Productos).ToList()
                })
                .OrderByDescending(r => r.Anio)
                .ThenByDescending(r => r.Mes)
                .ToList();

            return reporte;
        }
    }
}
