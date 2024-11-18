using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiServicioCupones.Data;
using ApiServicioCupones.Models;
using Serilog;

namespace ApiServicioCupones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreciosController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public PreciosController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Precios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrecioModel>>> GetPrecios()
        {
            Log.Information("Se consulto la lista de precios.");

            try
            {
                var precios = await _context.Precios.ToListAsync();

                return Ok(precios);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error al intentar obtener los precios.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los precios.");
            }
        }

        // GET: api/Precios/5
        [HttpGet("{id_Precio}")]
        public async Task<ActionResult<PrecioModel>> GetPrecioModel(int id_Precio)
        {
            Log.Information("Iniciando la operación para obtener el precio con ID {Id}", id_Precio);

            try
            {
                var precioModel = await _context.Precios.FindAsync(id_Precio);

                if (precioModel == null)
                {
                    Log.Information("No se encontró un precio con el ID {Id}", id_Precio);
                    return NotFound();
                }

                Log.Information("Precio con ID {Id} obtenido con éxito.", id_Precio);
                return Ok(precioModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error al intentar obtener el precio con ID {Id}", id_Precio);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el precio.");
            }
        }

        // PUT: api/Precios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id_Precio}")]
        public async Task<IActionResult> PutPrecioModel(int id_Precio, [FromBody] decimal precio)
        {
            // Buscar el precio con el ID proporcionado
            var precioModel = await _context.Precios.FindAsync(id_Precio);

            if (precioModel == null)
            {
                return NotFound(new { message = $"No se encontró un precio con el ID {id_Precio}." });
            }

            // Verificar que el precio sea un valor positivo antes de actualizar
            if (precio <= 0)
            {
                return BadRequest(new { message = "El precio debe ser un valor positivo." });
            }

            // Actualizar el precio
            precioModel.Precio = precio;

            // Guardar cambios en la base de datos
            _context.Precios.Update(precioModel);
            try
            {
                await _context.SaveChangesAsync();
                Log.Information("El precio con ID {Id_Precio} fue actualizado a {Precio}.", id_Precio, precio);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error al intentar actualizar el precio con ID {Id_Precio}.", id_Precio);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el precio.");
            }

            return NoContent(); // Respuesta exitosa, sin contenido
        }



        // POST: api/Precios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PrecioModel>> PostPrecioModel(PrecioModel precioModel)
        {

            try
            {
                _context.Precios.Add(precioModel);
                await _context.SaveChangesAsync();
                Log.Information("Nuevo precio creado exitosamente con ID {Id_Precio}.", precioModel.Id_Precio);

                return CreatedAtAction("GetPrecioModel", new { id = precioModel.Id_Precio }, precioModel);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error al intentar crear un nuevo precio.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el precio.");
            }
        }


        // DELETE: api/Precios/5
        [HttpDelete("{id_Precio}")]
        public async Task<IActionResult> DeletePrecioModel(int id_Precio)
        {
            var precioModel = await _context.Precios
                .Include(p => p.Articulo)
                .FirstOrDefaultAsync(p => p.Id_Precio == id_Precio);

            if (precioModel == null)
            {
                Log.Error("No se encontró un precio con el ID {Id_Precio} para realizar la eliminación.", id_Precio);
                return NotFound();
            }

            precioModel.Precio = 0;
            _context.Precios.Update(precioModel);

            try
            {
                await _context.SaveChangesAsync();
                Log.Information("El precio con ID {Id_Precio} fue eliminado exitosamente.", id_Precio);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error al intentar eliminar el precio con ID {Id_Precio}.", id_Precio);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al realizar la eliminación del precio.");
            }

            return NoContent();
        }



        private bool PrecioModelExists(int id_Precio)
        {
            return _context.Precios.Any(e => e.Id_Precio == id_Precio);
        }
    }
}
