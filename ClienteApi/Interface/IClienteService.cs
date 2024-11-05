using ClienteApi.Models.DTO;

namespace ClienteApi.Interface
{
    public interface IClienteService
    {
        Task<string> SolicitarCupon(ClienteDTO clienteDTO);
    }
}
