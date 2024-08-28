using FacturacionCLN.Models;

namespace FacturacionCLN.Repositories.Interfaces
{
    public interface ITasaCambioRepository
    {
        Task<IEnumerable<TasaCambio>> GetAllAsync();
        Task<TasaCambio> GetByIdAsync(int id);
        Task AddAsync(TasaCambio tasaCambio);
        Task UpdateAsync(TasaCambio tasaCambio);
        Task DeleteAsync(int id);
        Task<IEnumerable<TasaCambio>> GetTasaCambioByMonthAsync(int year, int month);
        Task<TasaCambio> GetTasaCambioOfTheDayAsync();
    }
}
