using ApiServicioCupones.Data;
using ApiServicioCupones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiServicioCupones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuemarCuponController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public QuemarCuponController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> QuemarCupon(string nroCupon)
        {
            try
            {
                var historial = await _context.Cupones_Historial
                                               .FirstOrDefaultAsync(h => h.NroCupon == nroCupon);

                if (historial != null)
                    return BadRequest("El cupón ya ha sido utilizado.");

                var cupon = await _context.Cupones_Clientes
                                           .FirstOrDefaultAsync(c => c.NroCupon == nroCupon);

                if (cupon == null)
                    return BadRequest("El cupón no existe o ya fue utilizado.");

                Cupon_HistorialModel nuevoHistorial = new Cupon_HistorialModel()
                {
                    Id_Cupon = cupon.Id_Cupon,
                    NroCupon = cupon.NroCupon,
                    CodCliente = cupon.CodCliente,
                    FechaUso = DateTime.Now
                };

                _context.Cupones_Historial.Add(nuevoHistorial);
                _context.Cupones_Clientes.Remove(cupon);

                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "El cupón fue utilizado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
