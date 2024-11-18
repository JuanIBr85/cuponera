using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiServicioCupones.Data;
using ApiServicioCupones.Models;

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
            // Verificar si la categoría existe
            var categoria = await _context.Categorias.FindAsync(idCategoria);
            if (categoria == null)
            {
                return NotFound(new { message = $"La categoría con ID {idCategoria} no existe." });
            }

            // Filtrar los artículos válidos
            var articulos = await _context.Articulos
                .Where(a => idArticulos.Contains(a.Id_Articulo))
                .ToListAsync();

            if (articulos.Count == 0)
            {
                return BadRequest(new { message = "No se encontraron artículos válidos en la lista proporcionada." });
            }

            // Asignar la categoría a los artículos
            foreach (var articulo in articulos)
            {
                articulo.Id_Categoria = idCategoria;
            }

            // Guardar los cambios en la base de datos
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = $"{articulos.Count} artículos fueron asignados a la categoría con ID {idCategoria}." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al asignar los artículos a la categoría.", details = ex.Message });
            }
        }
    }
}
