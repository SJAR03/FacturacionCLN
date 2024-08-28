using FacturacionCLN.Data;
using FacturacionCLN.DTO;
using FacturacionCLN.Models;
using FacturacionCLN.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FacturacionCLN.Services
{

    public class FacturaService : IFactura
    {
        private readonly FacturacionDbContext _context;

        public FacturaService(FacturacionDbContext context)
        {
            _context = context;
        }

        public FacturaDTO CrearFactura(FacturaDTO facturaDto)
        {
            try
            {
                // Validación de cliente
                var cliente = _context.Clientes.Find(facturaDto.IdCliente);
                if (cliente == null) throw new Exception("Cliente no encontrado");

                // Validación de tasa de cambio
                var tasaCambio = _context.TasaCambios
                                .Where(t => t.Fecha.Date == facturaDto.Fecha.Date)
                                .Select(t => t.Tasa)
                                .FirstOrDefault();

                if (tasaCambio == 0) throw new Exception("Tasa de cambio no encontrada para la fecha");

                // Mapeo del DTO a la entidad Factura
                var factura = MapearFactura(facturaDto, cliente, tasaCambio);

                // Guardar factura en la base de datos
                _context.Facturas.Add(factura);
                _context.SaveChanges();

                // Retornar la factura mapeada a DTO
                return MapearFacturaDTO(factura);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public bool EliminarFactura(int id)
        {
            var factura = _context.Facturas.Find(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<FacturaDTO> ListarFacturas()
        {
            return _context.Facturas
                           .Include(f => f.DetallesFactura)
                           .Include(f => f.Cliente)
                           .Select(f => MapearFacturaDTO(f))
                           .ToList();
        }

        public IEnumerable<FacturaDTO> ObtenerFacturaPorIdOCliente(string criterio)
        {
            // Intentar buscar por ID de factura
            if (int.TryParse(criterio, out int idFactura))
            {
                var factura = _context.Facturas
                                      .Include(f => f.DetallesFactura)
                                      .Include(f => f.Cliente)
                                      .FirstOrDefault(f => f.Id == idFactura);
                if (factura != null)
                    return new List<FacturaDTO> { MapearFacturaDTO(factura) }; // Retorna una lista con una única factura
            }

            // Buscar por coincidencias en el nombre del cliente
            var facturasPorCliente = _context.Facturas
                                              .Include(f => f.DetallesFactura)
                                              .Include(f => f.Cliente)
                                              .Where(f => f.Cliente.Nombre.Contains(criterio))
                                              .ToList();

            return facturasPorCliente.Select(f => MapearFacturaDTO(f)).ToList(); // Retorna todas las coincidencias
        }

        // Mapeo de DTO a Entidad
        private Factura MapearFactura(FacturaDTO facturaDto, Cliente cliente, decimal tasaCambio)
        {
            var factura = new Factura
            {
                Fecha = facturaDto.Fecha,
                Cliente = cliente,
                DetallesFactura = new List<DetalleFactura>()
            };

            // Variables para totales
            decimal subtotalCordoba = 0;
            decimal subtotalDolar = 0;

            // Procesar cada detalle
            foreach (var detalleDto in facturaDto.DetallesFactura)
            {
                var producto = _context.Productos.Find(detalleDto.IdProducto);
                if (producto == null) throw new Exception($"Producto no encontrado: {detalleDto.IdProducto}");

                var detalleFactura = new DetalleFactura
                {
                    Producto = producto,
                    Cantidad = detalleDto.Cantidad,
                    PrecioUnitarioCordoba = producto.PrecioCordoba,
                    PrecioUnitarioDolar = producto.PrecioDolar,
                    SubtotalCordoba = producto.PrecioCordoba * detalleDto.Cantidad,
                    SubtotalDolar = producto.PrecioDolar * detalleDto.Cantidad
                };

                subtotalCordoba += detalleFactura.SubtotalCordoba;
                subtotalDolar += detalleFactura.SubtotalDolar;

                factura.DetallesFactura.Add(detalleFactura);
            }

            // Calcular IVA y totales
            factura.SubTotalCordoba = subtotalCordoba;
            factura.IVACordoba = subtotalCordoba * 0.15M;
            factura.MontoTotalCordoba = factura.SubTotalCordoba + factura.IVACordoba;

            factura.SubTotalDolar = subtotalDolar;
            factura.IVADolar = subtotalDolar * 0.15M;
            factura.MontoTotalDolar = factura.SubTotalDolar + factura.IVADolar;

            return factura;
        }

        // Mapeo de Entidad a DTO
        private static FacturaDTO MapearFacturaDTO(Factura factura)
        {
            return new FacturaDTO
            {
                IdFactura = factura.Id,
                IdCliente = factura.IdCliente,
                NombreCliente = factura.Cliente.Nombre,
                Fecha = factura.Fecha,
                SubTotalCordoba = factura.SubTotalCordoba,
                IVACordoba = factura.IVACordoba,
                MontoTotalCordoba = factura.MontoTotalCordoba,
                SubTotalDolar = factura.SubTotalDolar,
                IVADolar = factura.IVADolar,
                MontoTotalDolar = factura.MontoTotalDolar,
                DetallesFactura = factura.DetallesFactura.Select(d => new DetalleFacturaDTO
                {
                    IdProducto = d.IdProducto,
                    Cantidad = d.Cantidad,
                    PrecioUnitarioCordoba = d.PrecioUnitarioCordoba,
                    PrecioUnitarioDolar = d.PrecioUnitarioDolar
                }).ToList()
            };
        }
    }
}