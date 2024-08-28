using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FacturacionCLN.Data;
using FacturacionCLN.Models;
using FacturacionCLN.Services;
using FacturacionCLN.DTO;

namespace FacturacionCLN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {

        private readonly FacturaService _facturaService;

        public FacturasController(FacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        // Crear factura
        [HttpPost]
        public IActionResult CrearFactura([FromBody]FacturaDTO facturaDTO)
        {
            try
            {
                var factura = _facturaService.CrearFactura(facturaDTO);
                return Ok(factura);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // Listar facturas
        [HttpGet]
        public IActionResult ListarFacturas()
        {
            var facturas = _facturaService.ListarFacturas();
            return Ok(facturas);
        }

        // Obtener factura por id o cliente
        [HttpGet("buscar/{criterio}")]
        public IActionResult ObtenerFacturaPorIdOCliente(string criterio)
        {
            var factura = _facturaService.ObtenerFacturaPorIdOCliente(criterio);
            if (factura.Any())
                return Ok(factura);
            return NotFound("No se encontraron facturas con el criterio proporcionado.");
        }

        // Eliminar factura
        [HttpDelete("{id}")]
        public IActionResult EliminarFactura(int id)
        {
            var result = _facturaService.EliminarFactura(id);
            if (result)
                return Ok("Factura eliminada exitosamente");
            return BadRequest("No se pudo eliminar la factura");
        }

    }
}
