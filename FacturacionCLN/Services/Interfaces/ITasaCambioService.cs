using FacturacionCLN.Models;

namespace FacturacionCLN.Services.Interfaces
{
    public interface ITasaCambioService
    {
        Task<IEnumerable<TasaCambio>> GetAllTasaCambioAsync();
        Task<TasaCambio> GetTasaCambioByIdAsync(int id);
        Task AddTasaCambioAsync(TasaCambio tasaCambio);
        Task<IEnumerable<TasaCambio>> GetTasaCambioByMonthAsync(int year, int month);
        Task<TasaCambio> GetTasaCambioOfTheDayAsync();
        Task UpdateTasaCambioAsync(TasaCambio tasaCambio);
        Task DeleteTasaCambioAsync(int id);
    }
}
