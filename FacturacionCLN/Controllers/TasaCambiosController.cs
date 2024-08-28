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
using FacturacionCLN.Services.Interfaces;

namespace FacturacionCLN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasaCambiosController : ControllerBase
    {
        private readonly ITasaCambioService _tasaCambioService;

        public TasaCambiosController(ITasaCambioService tasaCambioService)
        {
            _tasaCambioService = tasaCambioService;
        }

        // GET: api/TasaCambios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasaCambio>>> GetTasaCambios()
        {
            var tasas = await _tasaCambioService.GetAllTasaCambioAsync();
            return Ok(tasas);
        }

        // GET: api/TasaCambios/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TasaCambio>> GetTasaCambio(int id)
        {
            var tasaCambio = await _tasaCambioService.GetTasaCambioByIdAsync(id);
            if (tasaCambio == null)
            {
                return NotFound();
            }
            return Ok(tasaCambio);
        }

        // GET: api/TasasCambio/diaria
        [HttpGet("diaria")]
        public async Task<ActionResult<TasaCambio>> GetTasaCambioDiaria()
        {
            var tasaCambio = await _tasaCambioService.GetTasaCambioOfTheDayAsync();
            if (tasaCambio == null)
            {
                return NotFound("No se ha registrado la tasa de cambio del día.");
            }
            return Ok(tasaCambio);
        }

        // GET: api/TasasCambio/mes/{year}/{month}
        [HttpGet("mes/{year}/{month}")]
        public async Task<ActionResult<IEnumerable<TasaCambio>>> GetTasaCambioPorMes(int year, int month)
        {
            var tasas = await _tasaCambioService.GetTasaCambioByMonthAsync(year, month);
            if (tasas == null || !tasas.Any())
            {
                return NotFound("No se ha registrado la tasa de cambio en ese mes.");
            }
            return Ok(tasas);
        }



        // PUT: api/TasaCambios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasaCambio(int id, TasaCambio tasaCambio)
        {
            if (id != tasaCambio.Id)
            {
                return BadRequest();
            }

            try
            {
                await _tasaCambioService.UpdateTasaCambioAsync(tasaCambio);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // POST: api/TasaCambios
        [HttpPost]
        public async Task<ActionResult<TasaCambio>> PostTasaCambio(TasaCambio tasaCambio)
        {
            try
            {
                await _tasaCambioService.AddTasaCambioAsync(tasaCambio);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction(nameof(GetTasaCambio), new { id = tasaCambio.Id }, tasaCambio);
        }

        // DELETE: api/TasaCambios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasaCambio(int id)
        {
            await _tasaCambioService.DeleteTasaCambioAsync(id);
            return NoContent();
        }

    }
}
