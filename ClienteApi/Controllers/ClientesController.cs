using ClienteApi.Models.DTO;
using ClienteApi.Interface; 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClienteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clientesService;

        public ClientesController(IClienteService clientesService) // Usa IClienteService en lugar de ClientesService
        {
            _clientesService = clientesService;
        }

        [HttpPost]
        public async Task<IActionResult> EnviarSolicitudCupones([FromBody] ClienteDTO clienteDTO)
        {
            try
            {
                var respuesta = await _clientesService.SolicitarCupon(clienteDTO);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                return BadRequest(errorMessage);
            }
        }
    }
}
