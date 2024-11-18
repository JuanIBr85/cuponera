using ApiServicioCupones.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiServicioCupones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponesActivosController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public CuponesActivosController(DataBaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCuponesActivos(string codCliente)
        {
            if (string.IsNullOrEmpty(codCliente))
            {
                return BadRequest("El código de cliente es obligatorio.");
            }

            var cuponesActivos = await (from c in _context.Cupones
                                        join ch in _context.Cupones_Historial
                                        on c.Id_Cupon equals ch.Id_Cupon
                                        where c.Activo == true && ch.CodCliente == codCliente
                                        select new
                                        {
                                            c,   // tabla Cupones
                                            ch   // tabla Cupones_Historial
                                        }).ToListAsync();

            if (cuponesActivos == null || !cuponesActivos.Any())
            {
                return NotFound($"No se encontraron cupones activos para el cliente con código {codCliente}.");
            }

            return Ok(cuponesActivos);
        }
    }
}
