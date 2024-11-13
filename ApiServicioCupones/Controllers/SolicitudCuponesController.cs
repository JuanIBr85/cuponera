using ApiServicioCupones.Data;
using ApiServicioCupones.Interfaces;
using ApiServicioCupones.Models;
using ApiServicioCupones.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace ApiServicioCupones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudCuponesController : ControllerBase
    {
        private readonly DataBaseContext _context;
        private readonly ICuponesService _cuponesService;
        private readonly ISendEmailService _sendEmailService; 

        public SolicitudCuponesController (DataBaseContext context, ICuponesService cuponesService, ISendEmailService sendEmailService)
        {

            _context = context;
            _cuponesService = cuponesService;
            _sendEmailService = sendEmailService;
        }

        [HttpPost("SolicitarCupon")]

        public async Task<IActionResult> SolicitarCupon(ClienteDto clienteDto)
        {
            try 
            {
                if (clienteDto.CodCliente.IsNullOrEmpty())
                    throw new Exception("El DNI del cliente no puede estar vacio");

                string nroCupon = await _cuponesService.GenerarNroCupon();

                Cupon_ClienteModel cupon_Cliente = new Cupon_ClienteModel()
                {
                Id_Cupon = clienteDto.id_Cupon,
                CodCliente = clienteDto.CodCliente,
                FechaAsignado = DateTime.Now,
                NroCupon = nroCupon

                };

                _context.Cupones_Clientes.Add(cupon_Cliente);
                await _context.SaveChangesAsync();

                await _sendEmailService.EnviarEmailCliente(clienteDto.Email, nroCupon);

                return Ok(new
                {
                    Msj = "Se dio de alta el registro correctamente.",
                    NroCupon = nroCupon
                });


            }
            catch (Exception ex)

            { 
                return BadRequest($"Error: {ex.Message}");
            }


        }
        /*
        [HttpPost("QuemadoCupon")]

        public async Task<IActionResult> QuemadoCupon( string nroCupon)
        {
            /* (POST): Este endpoint debe manejar el quemado del cupón
            de un cliente tras haber sido usado por el mismo. 
            El endpoint recibirá como parámetro el número de cupón(111 - 111 - 111) que
            va a ser utilizado.
            El flujo es:
                        i.Recibir número de cupón.
                        ii.Insertar registro en Cupones_Historial.
                        iii.Eliminar registro en Cupones_Clientes.
                        iv.Devolver mensaje indicando que el cupón fue utilizado correctamente.
          
        }*/

        [HttpPost("QuemadoCupon")]
        public async Task<IActionResult> QuemadoCupon([FromBody] string nroCupon)
        {
            try
            {
                var cuponCliente = await _context.Cupones_Clientes
                    .FirstOrDefaultAsync(c => c.NroCupon == nroCupon);

                if (cuponCliente == null)
                    return NotFound("El cupón no existe o ya fue utilizado.");

                var cuponHistorial = new Cupon_HistorialModel
                {
                    Id_Cupon = cuponCliente.Id_Cupon,
                    NroCupon = nroCupon,
                    FechaUso = DateTime.Now
                };

                _context.Cupones_Historial.Add(cuponHistorial);
                _context.Cupones_Clientes.Remove(cuponCliente);

                await _context.SaveChangesAsync();

                return Ok(new { Msj = $"El cupón {nroCupon} fue utilizado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
