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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetArticulos()
        {
            var articulos = await _context.Articulos
                                          .Include(art => art.Categoria) // Carga la relación con Categorias
                                          .Select(art => new
                                          {
                                              art.Id_Articulo,
                                              art.Nombre_Articulo,
                                              art.Descripcion_Articulo,
                                              art.Activo,
                                              CategoriaNombre = art.Categoria != null ? art.Categoria.Nombre : null,
                                              Precio = art.Precio != null ? (decimal?)art.Precio.Precio : null
                                          })
                                          .ToListAsync();

            return Ok(articulos);
        }


        // GET: api/Articulo/5
        [HttpGet("{id_articulo}")]
        public async Task<ActionResult<ArticuloModel>> GetArticuloModel(int id_articulo)
        {
            var articuloModel = await _context.Articulos
                .Include(a => a.Precio)
                .FirstOrDefaultAsync(a => a.Id_Articulo == id_articulo);

            if (articuloModel == null)
            {
                return NotFound(new { message = $"El artículo con ID {id_articulo} no fue encontrado." });
            }

            return articuloModel;
        }



        // PUT: api/Articulo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticuloModel(int id, ArticuloModel articuloModel, [FromQuery] decimal precio)
        {
            // Verificar si el artículo existe
            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null)
            {
                return NotFound(new { message = $"El artículo con ID {id} no fue encontrado." });
            }

            // Actualizar el artículo con los valores recibidos
            articulo.Nombre_Articulo = articuloModel.Nombre_Articulo;
            articulo.Descripcion_Articulo = articuloModel.Descripcion_Articulo;
            articulo.Activo = articuloModel.Activo;
            articulo.Id_Categoria = articuloModel.Id_Categoria;

            // Verificar si el precio ha cambiado y actualizarlo si es necesario
            if (precio > 0)
            {
                var precioModel = await _context.Precios
                                                .FirstOrDefaultAsync(p => p.Id_Articulo == id);

                if (precioModel != null)
                {
                    precioModel.Precio = precio;
                    _context.Precios.Update(precioModel);
                }
                else
                {
                    // Si no existe un precio asociado, agregar uno nuevo
                    var newPrecio = new PrecioModel
                    {
                        Id_Articulo = id,
                        Precio = precio
                    };
                    _context.Precios.Add(newPrecio);
                }
            }

            _context.Articulos.Update(articulo);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        // POST: api/Articulo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArticuloModel>> PostArticuloModel(ArticuloModel articuloModel, decimal precio)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Verificar si la categoría existe
                var categoriaExistente = await _context.Categorias
                                                       .FirstOrDefaultAsync(c => c.Id_Categoria == articuloModel.Id_Categoria);

                if (categoriaExistente == null)
                {
                    return BadRequest("La categoría especificada no existe.");
                }

                // Agregar el artículo
                _context.Articulos.Add(articuloModel);
                await _context.SaveChangesAsync();

                // Crear y asociar el precio
                var precioModel = new PrecioModel
                {
                    Id_Articulo = articuloModel.Id_Articulo,
                    Precio = precio
                };

                _context.Precios.Add(precioModel);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                // Devolver el artículo creado
                return CreatedAtAction("GetArticuloModel", new { id_articulo = articuloModel.Id_Articulo }, articuloModel);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Error al guardar el artículo y su precio.");
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
