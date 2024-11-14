using ClienteApi.Models.DTO;

namespace ClienteApi.Interface
{
    public interface IClienteService
    {
        Task<string> SolicitarCupon(ClienteDTO clienteDTO);
        Task<string> QuemarCuponAsync(string nroCupon);
        Task<string> ObtenerCuponesActivosAsync(string codCliente);
    }
}
