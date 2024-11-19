using ClienteApi.Models.DTO;
using ClienteApi.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Serilog;

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
                Log.Information("Solicitud de cupón para el cliente con código: {CodCliente}", clienteDTO.CodCliente);

                var respuesta = await _clientesService.SolicitarCupon(clienteDTO);

                Log.Information("Solicitud de cupón realizada con éxito, cliente con código: {CodCliente}", clienteDTO.CodCliente);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                Log.Error("Error al solicitar cupón para el cliente con código: {CodCliente}. Detalle del error: {ErrorMessage}", clienteDTO.CodCliente, errorMessage);
                return BadRequest(errorMessage);
            }
        }


        [HttpPost("QuemarCupon")]
        public async Task<IActionResult> QuemarCupon([FromQuery] string nroCupon)
        {
            try
            {
                var respuesta = await _clientesService.QuemarCuponAsync(nroCupon);

                Log.Information("Cupón con número {NroCupon} quemado exitosamente.", nroCupon);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                Log.Error("Error al quemar el cupón con número {NroCupon}. Detalles: {ErrorMessage}", nroCupon, errorMessage);
                return BadRequest(errorMessage);
            }
        }


        [HttpGet("CuponesActivos")]
        public async Task<IActionResult> ObtenerCuponesActivos([FromQuery] string codCliente)
        {
            try
            {

                var respuesta = await _clientesService.ObtenerCuponesActivosAsync(codCliente);

                Log.Information("Obtención de cupones activos para el cliente con código: {CodCliente}", codCliente);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Error: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }

                Log.Error("Error al obtener cupones activos para el cliente {CodCliente}. Detalles: {ErrorMessage}", codCliente, errorMessage);
                return BadRequest(errorMessage);
            }
        }

    }
}
