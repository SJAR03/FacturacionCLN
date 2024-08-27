using FacturacionCLN.Models;

namespace FacturacionCLN.Services.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllClientesAsync();
        Task<Cliente> GetClienteByIdAsync(int id);
        Task<IEnumerable<Cliente>> SearchClientesAsync(string nombre, string codigo);
        Task AddClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(Cliente cliente);
        Task DeleteClienteAsync(int id);
        Task<bool> ClienteExistsAsync(int id);
    }
}
