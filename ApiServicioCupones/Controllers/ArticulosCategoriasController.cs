using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiServicioCupones.Data;
using ApiServicioCupones.Models;
using Serilog;

namespace ApiServicioCupones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosCategoriasController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public ArticulosCategoriasController(DataBaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asigna uno o más artículos a una categoría específica.
        /// </summary>
        /// <param name="idCategoria">ID de la categoría a la que se asignarán los artículos.</param>
        /// <param name="idArticulos">Lista de IDs de los artículos que se asignarán a la categoría.</param>
        /// <returns>Respuesta HTTP indicando el resultado de la operación.</returns>
        [HttpPost("{idCategoria}/asignar-articulos")]
        public async Task<IActionResult> AsignarArticulosACategoria(int idCategoria, [FromBody] List<int> idArticulos)
        {
            try
            {
                var categoria = await _context.Categorias.FindAsync(idCategoria);
                if (categoria == null)
                {
                    Log.Information($"La categoría con ID {idCategoria} no existe.");
                    return NotFound($"La categoría con ID {idCategoria} no existe.");
                }

                var articulos = await _context.Articulos
                    .Where(a => idArticulos.Contains(a.Id_Articulo))
                    .ToListAsync();

                if (articulos.Count == 0)
                {
                    Log.Information($"No se encontraron artículos para asignar a la categoría con ID {idCategoria}.");
                    return BadRequest("No se encontraron artículos válidos en la lista proporcionada.");
                }

                foreach (var articulo in articulos)
                {
                    articulo.Id_Categoria = idCategoria;
                }

                await _context.SaveChangesAsync();
                Log.Information($"{articulos.Count} artículos fueron asignados a la categoría con ID {idCategoria}.");
                return Ok($"{articulos.Count} artículos fueron asignados a la categoría con ID {idCategoria}.");
            }
            catch (Exception ex)
            {
                Log.Error($"Error al asignar los artículos a la categoría con ID {idCategoria}, error: {ex.Message}");
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }

    }
}
