using FacturacionCLN.Models;
using FacturacionCLN.Repositories.Interfaces;
using FacturacionCLN.Services.Interfaces;

namespace FacturacionCLN.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
        {
            return await _clienteRepository.GetAllAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            return await _clienteRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Cliente>> SearchClientesAsync(string nombre, string codigo)
        {
            return await _clienteRepository.SearchAsync(nombre, codigo);
        }

        public async Task AddClienteAsync(Cliente cliente)
        {
            await _clienteRepository.AddAsync(cliente);
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task DeleteClienteAsync(int id)
        {
            await _clienteRepository.DeleteAsync(id);
        }

        public async Task<bool> ClienteExistsAsync(int id)
        {
            return await _clienteRepository.ExistsAsync(id);
        }

    }
}
