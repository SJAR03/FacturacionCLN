using FacturacionCLN.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FacturacionCLN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ReporteVentasService _reporteVentasService;

        public ReportesController(ReporteVentasService reporteVentasService)
        {
            _reporteVentasService = reporteVentasService;
        }

        [HttpGet("ventas-mensuales")]
        public IActionResult ObtenerReporteVentasMensuales(
            [FromQuery] int? codigoCliente = null,
            [FromQuery] string nombreCliente = null,
            [FromQuery] int? anio = null,
            [FromQuery] int? mes = null,
            [FromQuery] string producto = null,
            [FromQuery] string sku = null)        
        {
            var reporte = _reporteVentasService.GenerarReporteVentasMensuales(codigoCliente, nombreCliente, anio, mes, producto, sku);
            if (reporte.Any())
                return Ok(reporte);
            return NotFound("No se encontraron datos para el reporte con los criterios proporcionados.");
        }
    }
}
