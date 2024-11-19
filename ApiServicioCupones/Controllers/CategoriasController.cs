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
    public class CategoriasController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public CategoriasController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaModel>>> GetCategorias()
        {
            try
            {
                Log.Information("Iniciando la obtención de categorías.");

                var categorias = await _context
                    .Categorias
                    .Include(c => c.Cupones_Categorias)
                        .ThenInclude(cc => cc.Cupon)
                    .ToListAsync();

                Log.Information("Se obtuvieron {Count} categorías correctamente.", categorias.Count);

                return categorias;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error al obtener las categorías.");
                return StatusCode(500, "Error al obtener las categorías.");
            }
        }

        // GET: api/Categorias/5
        [HttpGet("{id_Categoria}")]
        public async Task<ActionResult<CategoriaModel>> GetCategoriaModel(int id_Categoria)
        {
            try
            {

                var categoriaModel = await _context.Categorias.FindAsync(id_Categoria);

                if (categoriaModel == null)
                {
                    Log.Information("La categoría con ID {Id_Categoria} no fue encontrada.", id_Categoria);
                    return NotFound($"La categoría con ID {id_Categoria} no fue encontrada.");
                }

                Log.Information("La categoría con ID {Id_Categoria} fue encontrada.", id_Categoria);
                return categoriaModel;
            }
            catch (Exception ex)
            {
                Log.Error($"Hubo un problema al buscar la categoria solicitada, error: {ex.Message}");
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }


        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id_Categoria}")]
        public async Task<IActionResult> PutCategoriaModel(int id_Categoria, CategoriaModel categoriaModel)
        {
            try
            {
                // Verificar si el ID proporcionado coincide con el del modelo
                if (id_Categoria != categoriaModel.Id_Categoria)
                {
                    Log.Error("El ID de la categoría proporcionado ({Id_Categoria}) no coincide con el ID del modelo ({Modelo_Id_Categoria}).", id_Categoria, categoriaModel.Id_Categoria);
                    return BadRequest();
                }

                // Actualizar el estado del modelo
                _context.Entry(categoriaModel).State = EntityState.Modified;

                // Guardar los cambios
                await _context.SaveChangesAsync();
                Log.Information("Categoría con ID {Id_Categoria} actualizada correctamente.", id_Categoria);

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaModelExists(id_Categoria))
                {
                    Log.Error($"Hubo un problema, la categoría con ID {id_Categoria} no fue encontrada.");
                    return NotFound();
                }
                else
                {
                    Log.Error($"Hubo un problema, error de concurrencia al intentar actualizar la categoría con ID {id_Categoria}.");
                    throw;
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Hubo un problema en la actualizacion de la categoria, error: {ex.Message}");
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }


        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriaModel>> PostCategoriaModel(CategoriaModel categoriaModel)
        {
            try
            {
                _context.Categorias.Add(categoriaModel);
                await _context.SaveChangesAsync();

                Log.Information("Categoría con ID {Id_Categoria} creada correctamente.", categoriaModel.Id_Categoria);

                return CreatedAtAction("GetCategoriaModel", new { id_Categoria = categoriaModel.Id_Categoria }, categoriaModel);
            }
            catch (Exception ex)
            {
                Log.Error($"Hubo un problema al crear la categoría, error: {ex.Message}");
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }

        // DELETE: api/Categorias/5
        // DELETE: api/Categorias/5
        [HttpDelete("{id_Categoria}")]
        public async Task<IActionResult> DeleteCategoriaModel(int id_Categoria)
        {
            try
            {
                var categoriaModel = await _context.Categorias
                    .Include(c => c.Cupones_Categorias) 
                    .FirstOrDefaultAsync(c => c.Id_Categoria == id_Categoria);

                if (categoriaModel == null)
                {
                    Log.Error($"La categoría con ID {id_Categoria} no fue encontrada.");
                    return NotFound();
                }

                _context.Cupones_Categorias.RemoveRange(categoriaModel.Cupones_Categorias);


                _context.Categorias.Remove(categoriaModel);
                await _context.SaveChangesAsync();

                Log.Information("Categoría con ID {Id_Categoria} eliminada correctamente.", id_Categoria);

                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error($"Hubo un problema al eliminar la categoría con ID {id_Categoria}, error: {ex.Message}");
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }



        private bool CategoriaModelExists(int id_Categoria)
        {
            return _context.Categorias.Any(e => e.Id_Categoria == id_Categoria);
        }
    }
}
