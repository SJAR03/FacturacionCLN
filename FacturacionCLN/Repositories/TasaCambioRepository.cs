using FacturacionCLN.Data;
using FacturacionCLN.Models;
using FacturacionCLN.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FacturacionCLN.Repositories
{
    public class TasaCambioRepository : ITasaCambioRepository
    {
        private readonly FacturacionDbContext _context;

        public TasaCambioRepository(FacturacionDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TasaCambio>> GetAllAsync()
        {
            return await _context.TasaCambios.ToListAsync();
        }

        public async Task<TasaCambio> GetByIdAsync(int id)
        {
            return await _context.TasaCambios.FindAsync(id);
        }

        public async Task AddAsync(TasaCambio tasaCambio)
        {
            _context.TasaCambios.Add(tasaCambio);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TasaCambio>> GetTasaCambioByMonthAsync(int year, int month)
        {
            return await _context.TasaCambios
                .Where(tc => tc.Fecha.Year == year && tc.Fecha.Month == month)
                .ToListAsync();
        }

        public async Task<TasaCambio> GetTasaCambioOfTheDayAsync()
        {
            var today = DateTime.Today;
            return await _context.TasaCambios
                .FirstOrDefaultAsync(tc => tc.Fecha.Date == today);
        }

        public async Task UpdateAsync(TasaCambio tasaCambio)
        {
            _context.Entry(tasaCambio).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tasaCambio = await _context.TasaCambios.FindAsync(id);
            if (tasaCambio != null)
            {
                _context.TasaCambios.Remove(tasaCambio);
                await _context.SaveChangesAsync();
            }
        }
    }
}
