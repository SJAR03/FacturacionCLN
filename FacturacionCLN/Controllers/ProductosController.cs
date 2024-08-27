using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FacturacionCLN.Data;
using FacturacionCLN.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FacturacionCLN.Services;

namespace FacturacionCLN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly FacturacionDbContext _context;
        private readonly ProductoService _prodService;

        public ProductosController(FacturacionDbContext context, ProductoService productoService)
        {
            _context = context;
            _prodService = productoService;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            // Solo listar los productos activos
            return await _context.Productos.Where(p => p.Activo == true).ToListAsync();
        }

        // GET: api/Productos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // GET: api/Productos/search?sku={sku}&descripcion={descripcion}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Producto>>> SearchProductos([FromQuery] string? sku = null, [FromQuery] string? descripcion = null)
        {
            var query = _context.Productos.AsQueryable();

            if (!string.IsNullOrEmpty(sku))
            {
                query = query.Where(p => EF.Functions.Like(p.SKU, $"%{sku}%"));
            }

            if (!string.IsNullOrEmpty(descripcion))
            {
                query = query.Where(p => EF.Functions.Like(p.Descripcion, $"%{descripcion}%"));
            }

            var productos = await query.ToListAsync();

            if (productos == null || productos.Count == 0)
            {
                return NotFound();
            }

            // Obtener la tasa de cambio del dia
            var tasaCambioHoy = await ObtenerTasaCambioDelDia();

            if (tasaCambioHoy == 0)
            {
                // Manejar el caso en que no se haya registrado la tasa de cambio del dia
                return BadRequest("No se ha registrado la tasa de cambio del dia");
            }

            // Devolver los productos junto con la tasa de cambio
            var productosConTasa = productos.Select(p => new
            {
                p.Id,
                p.SKU,
                p.Descripcion,
                p.PrecioCordoba,
                p.PrecioDolar,
                TasaCambio = tasaCambioHoy,
                p.Activo
            });

            return Ok(productosConTasa);

            //return productos;
        }


        // PUT: api/Productos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            // Validar si el SKU ya existe
            if (_prodService.SkuExiste(producto.SKU))
            {
                return BadRequest("El SKU ingresado ya existe. Por favor, elija un SKU diferente.");
            }

            var tasaCambioHoy = await ObtenerTasaCambioDelDia();
            if (tasaCambioHoy == 0)
            {
                // Manejar el caso en que no se haya registrado la tasa de cambio del dia
                return BadRequest("No se ha registrado la tasa de cambio del dia");
            }

            // Validar los precios y realizar la conversion si es necesario usando el servicio ProductoService
            string validacion = _prodService.ValidarPrecios(ref producto, tasaCambioHoy);
            if (validacion != null)
            {
                return BadRequest(validacion);
            }

                _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {

            // Validar si el SKU ya existe
            if (_prodService.SkuExiste(producto.SKU))
            {
                return BadRequest("El SKU ingresado ya existe. Por favor, elija un SKU diferente.");
            }

            var tasaCambioHoy = await ObtenerTasaCambioDelDia();
            if (tasaCambioHoy == 0)
            {
                return BadRequest("No se encontró una tasa de cambio para el día.");
            }

            // Validar precios y realizar conversiones usando el servicio
            string validacion = _prodService.ValidarPrecios(ref producto, tasaCambioHoy);
            if (validacion != null)
            {
                return BadRequest(validacion);
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
        }

        // DELETE: api/Productos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Obtener la tasa de cambio del dia
        private async Task<decimal> ObtenerTasaCambioDelDia()
        {
            return await _context.TasaCambios
                .Where(tc => tc.Fecha == DateTime.Today)
                .Select(tc => tc.Tasa)
                .FirstOrDefaultAsync();
        }

        // Validar que el id sea unico
        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
