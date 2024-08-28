using FacturacionCLN.Models;
using FacturacionCLN.Repositories;
using FacturacionCLN.Repositories.Interfaces;
using FacturacionCLN.Services.Interfaces;

namespace FacturacionCLN.Services
{
    public class TasaCambioService : ITasaCambioService
    {
        private readonly ITasaCambioRepository _tasaCambioRepository;

        public TasaCambioService(ITasaCambioRepository tasaCambioRepository)
        {
            _tasaCambioRepository = tasaCambioRepository;
        }

        public async Task<IEnumerable<TasaCambio>> GetAllTasaCambioAsync()
        {
            return await _tasaCambioRepository.GetAllAsync();
        }

        public async Task<TasaCambio> GetTasaCambioByIdAsync(int id)
        {
            return await _tasaCambioRepository.GetByIdAsync(id);
        }

        public async Task AddTasaCambioAsync(TasaCambio tasaCambio)
        {
            var (isValid, errorMessage) = ValidarTasaCambio(tasaCambio.Tasa);
            if (!isValid)
            {
                throw new ArgumentException(errorMessage);
            }

            await _tasaCambioRepository.AddAsync(tasaCambio);
        }

        public async Task<IEnumerable<TasaCambio>> GetTasaCambioByMonthAsync(int year, int month)
        {
            return await _tasaCambioRepository.GetTasaCambioByMonthAsync(year, month);
        }

        public async Task<TasaCambio> GetTasaCambioOfTheDayAsync()
        {
            return await _tasaCambioRepository.GetTasaCambioOfTheDayAsync();
        }

        public async Task UpdateTasaCambioAsync(TasaCambio tasaCambio)
        {
            var (isValid, errorMessage) = ValidarTasaCambio(tasaCambio.Tasa);
            if (!isValid)
            {
                throw new ArgumentException(errorMessage);
            }

            await _tasaCambioRepository.UpdateAsync(tasaCambio);
        }

        public async Task DeleteTasaCambioAsync(int id)
        {
            await _tasaCambioRepository.DeleteAsync(id);
        }

        private (bool IsValid, string ErrorMessage) ValidarTasaCambio(decimal tasaCambio)
        {
            if (tasaCambio <= 0)
            {
                return (false, "La tasa de cambio debe ser un número positivo.");
            }
            return (true, null);
        }
    }
}
