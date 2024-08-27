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

namespace FacturacionCLN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasaCambiosController : ControllerBase
    {
        private readonly FacturacionDbContext _context;
        private readonly TasaCambioService _tasaCambioService;

        public TasaCambiosController(FacturacionDbContext context, TasaCambioService tasaCambioService)
        {
            _context = context;
            _tasaCambioService = tasaCambioService;
        }

        // GET: api/TasaCambios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasaCambio>>> GetTasaCambios()
        {
            return await _context.TasaCambios.ToListAsync();
        }

        // GET: api/TasaCambios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TasaCambio>> GetTasaCambio(int id)
        {
            var tasaCambio = await _context.TasaCambios.FindAsync(id);

            if (tasaCambio == null)
            {
                return NotFound();
            }

            return tasaCambio;
        }

        // GET: api/TasasCambio/diaria
        [HttpGet("diaria")]
        public async Task<ActionResult<TasaCambio>> GetTasaCambioDiaria()
        {
            var hoy = DateTime.Today;
            var tasaCambioHoy = await _context.TasaCambios.FirstOrDefaultAsync(tc => tc.Fecha.Date == hoy);

            if (tasaCambioHoy == null)
            {
                return NotFound("No se ha registrado la tasa de cambio del dia");
            }

            return tasaCambioHoy;
        }

        // GET: api/TasasCambio/mes/{year}/{month}
        [HttpGet("mes/{year}/{month}")]
        public async Task<ActionResult<IEnumerable<TasaCambio>>> GetTasaCambioPorMes(int year, int month)
        {
            var tasaCambioMes = await _context.TasaCambios
                .Where(tc => tc.Fecha.Year == year && tc.Fecha.Month == month)
                .ToListAsync();

            if (!tasaCambioMes.Any())
            {
                return NotFound("No se ha registrado la tasa de cambio en ese mes");
            }

            return tasaCambioMes;
        }



        // PUT: api/TasaCambios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasaCambio(int id, TasaCambio tasaCambio)
        {
            if (id != tasaCambio.Id)
            {
                return BadRequest();
            }

            // Validación de la tasa de cambio
            var (tasaEsValida, mensajeErrorTasa) = _tasaCambioService.ValidarTasaCambio(tasaCambio.Tasa);

            if (!tasaEsValida)
            {
                return BadRequest(mensajeErrorTasa);
            }

            _context.Entry(tasaCambio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasaCambioExists(id))
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

        // POST: api/TasaCambios
        [HttpPost]
        public async Task<ActionResult<TasaCambio>> PostTasaCambio(TasaCambio tasaCambio)
        {
            // Validación de la tasa de cambio
            var (tasaEsValida, mensajeErrorTasa) = _tasaCambioService.ValidarTasaCambio(tasaCambio.Tasa);

            if (!tasaEsValida)
            {
                return BadRequest(mensajeErrorTasa);
            }

            _context.TasaCambios.Add(tasaCambio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTasaCambio", new { id = tasaCambio.Id }, tasaCambio);
        }

        // DELETE: api/TasaCambios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasaCambio(int id)
        {
            var tasaCambio = await _context.TasaCambios.FindAsync(id);
            if (tasaCambio == null)
            {
                return NotFound();
            }

            _context.TasaCambios.Remove(tasaCambio);
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

        private bool TasaCambioExists(int id)
        {
            return _context.TasaCambios.Any(e => e.Id == id);
        }
    }
}
