using ApiServicioCupones.Data;
using ApiServicioCupones.Interfaces;
using ApiServicioCupones.Models;
using ApiServicioCupones.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
