using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FacturacionCLN.Data;
using FacturacionCLN.Models;
using FacturacionCLN.Services.Interfaces;

namespace FacturacionCLN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _clienteService.GetAllClientesAsync();
            return Ok(clientes);
        }

        // GET: api/Clientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteService.GetClienteByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        // Get: api/Clientes/search?nombre={nombre}&codigo={codigo}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Cliente>>> SearchClientes([FromQuery] string? nombre = null, [FromQuery] string? codigo = null)
        {
            var clientes = await _clienteService.SearchClientesAsync(nombre, codigo);
            if (clientes == null || !clientes.Any())
            {
                return NotFound();
            }
            return Ok(clientes);
        }

        // PUT: api/Clientes/{5}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            if (!await _clienteService.ClienteExistsAsync(id))
            {
                return NotFound();
            }

            await _clienteService.UpdateClienteAsync(cliente);
            return NoContent();
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            await _clienteService.AddClienteAsync(cliente);
            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            if (!await _clienteService.ClienteExistsAsync(id))
            {
                return NotFound();
            }

            await _clienteService.DeleteClienteAsync(id);
            return NoContent();
        }

    }
}
