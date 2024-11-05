using ClienteApi.Models.DTO;
using ClienteApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClienteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClientesService _clientesService;

        public ClientesController (ClientesService clientesService)
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
                return BadRequest($"Error: {ex.Message}");

            }

        }
    }
}
