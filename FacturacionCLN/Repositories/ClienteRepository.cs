using FacturacionCLN.Data;
using FacturacionCLN.Models;
using FacturacionCLN.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FacturacionCLN.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly FacturacionDbContext _context;

        public ClienteRepository(FacturacionDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<IEnumerable<Cliente>> SearchAsync(string nombre, string codigo)
        {
            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(c => EF.Functions.Like(c.Nombre, $"%{nombre}%"));
            }

            if (!string.IsNullOrEmpty(codigo))
            {
                query = query.Where(c => EF.Functions.Like(c.Codigo, $"%{codigo}%"));
            }

            return await query.ToListAsync();
        }

        public async Task AddAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Clientes.AnyAsync(e => e.Id == id);
        }

    }
}
