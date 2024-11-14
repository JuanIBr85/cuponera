using ClienteApi.Models.DTO;
using ClienteApi.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace ClienteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clientesService;

        public ClientesController(IClienteService clientesService)
        {
            _clientesService = clientesService;
        }

        [HttpPost("SolicitarCupon")]
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

        [HttpPost("QuemarCupon")]
        public async Task<IActionResult> QuemarCupon([FromQuery] string nroCupon)
        {
            try
            {
                var respuesta = await _clientesService.QuemarCuponAsync(nroCupon);
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
        [HttpGet("CuponesActivos")]
        public async Task<IActionResult> ObtenerCuponesActivos([FromQuery] string codCliente)
        {
            try
            {
                var respuesta = await _clientesService.ObtenerCuponesActivosAsync(codCliente);
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
