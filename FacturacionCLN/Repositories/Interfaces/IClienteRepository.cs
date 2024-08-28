using FacturacionCLN.Models;

namespace FacturacionCLN.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente> GetByIdAsync(int id);
        Task<IEnumerable<Cliente>> SearchAsync(string nombre, string codigo);
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
