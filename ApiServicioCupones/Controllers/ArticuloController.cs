using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiServicioCupones.Data;
using ApiServicioCupones.Models;

namespace ApiServicioCupones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticuloController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public ArticuloController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Articulo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticuloModel>>> GetArticulos()
        {
            return await _context.Articulos.ToListAsync();
               // .Include(art => art.Precio)
        }

        // GET: api/Articulo/5
        [HttpGet("{id_articulo}")]
        public async Task<ActionResult<ArticuloModel>> GetArticuloModel(int id_articulo)
        {
            var articuloModel = await _context.Articulos
                      //.Include(art => art.Precio)
                       .FirstOrDefaultAsync(art => art.Id_Articulo == id_articulo);

            if (articuloModel == null)
            {
                return NotFound();
            }

            return articuloModel;
        }

        // PUT: api/Articulo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticuloModel(int id, ArticuloModel articuloModel)
        {
            if (id != articuloModel.Id_Articulo)
            {
                return BadRequest();
            }

            _context.Entry(articuloModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticuloModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Articulo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArticuloModel>> PostArticuloModel(ArticuloModel articuloModel)
        {
            try
            {
                // No debes asignar un valor a Id_Articulo. SQL Server lo generará automáticamente.
                _context.Articulos.Add(articuloModel);
                await _context.SaveChangesAsync();

                // El Id_Articulo se genera automáticamente después de la inserción
                // Devolvemos el artículo creado con el Id_Articulo recién generado
                return CreatedAtAction("GetArticuloModel", new { id_articulo = articuloModel.Id_Articulo }, articuloModel);
            }
            catch (Exception ex)
            {
                // Log o despliegue del error
                Console.WriteLine($"Error al guardar el artículo: {ex.Message}");
                return StatusCode(500, "Error al guardar el artículo.");
            }
        }


        // DELETE: api/Articulo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticuloModel(int id)
        {
            var articuloModel = await _context.Articulos.FindAsync(id);
            if (articuloModel == null)
            {
                return NotFound();
            }

            articuloModel.Activo = false;

            _context.Articulos.Update(articuloModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticuloModelExists(int id)
        {
            return _context.Articulos.Any(e => e.Id_Articulo == id);
        }
    }
}
