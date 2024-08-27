using FacturacionCLN.Data;
using FacturacionCLN.Models;

namespace FacturacionCLN.Services
{
    public class ProductoService
    {

        private readonly FacturacionDbContext _context;

        public ProductoService(FacturacionDbContext context)
        {
            _context = context;
        }

        // Validar que los precios ingresados sean coherentes
        public string ValidarPrecios(ref Producto producto, decimal tasaCambioHoy)
        {
            if (!PrecioConDosDecimales(producto.PrecioCordoba))
            {
                return "El precio en córdobas debe tener dos decimales";
            }

            if (!PrecioConDosDecimales(producto.PrecioDolar))
            {
                return "El precio en dólares debe tener dos decimales";
            }

            // Si el precio en cordobas fue ingresado y el precio en dolares no, se calcula el precio en dolares
            if (producto.PrecioCordoba != 0 && producto.PrecioDolar == 0)
            {
                producto.PrecioDolar = Math.Round(producto.PrecioCordoba / tasaCambioHoy, 2);
            }

            // Si el precio en dolares fue ingresado y el precio en cordobas no, se calcula el precio en cordobas
            if (producto.PrecioDolar != 0 && producto.PrecioCordoba == 0)
            {
                producto.PrecioCordoba = Math.Round(producto.PrecioDolar * tasaCambioHoy, 2);
            }

            // Si ambos precios fueron ingresados, se valida que sean coherentes
            else if (producto.PrecioCordoba != 0 && producto.PrecioDolar != 0)
            {
                var precioDolarCalculado = Math.Round(producto.PrecioCordoba / tasaCambioHoy, 2);
                if (precioDolarCalculado != producto.PrecioDolar)
                {
                    return "Los precios ingresados no coinciden con la tasa de cambio del día";
                }
            }

            return null;

        }

        public bool SkuExiste(string sku, int? idProducto = null)
        {
            return _context.Productos.Any(p => p.SKU == sku && p.Id != idProducto);
        }

        // Validar que el precio tenga dos decimales
        private bool PrecioConDosDecimales(decimal numero)
        {
            return decimal.Round(numero, 2) == numero;
        }
    }
}
