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
            try
            {
                var articulos = await _context.Articulos
                                              .Include(art => art.Categoria)
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

                Log.Information("Se obtuvieron {Count} artículos.", articulos.Count);
                return Ok(articulos);
            }
            catch (Exception ex)
            {
                Log.Error($"Hubo un problema al obtener los articulos, error: {ex.Message}");
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }


        // GET: api/Articulo/5
        [HttpGet("{id_articulo}")]
        public async Task<ActionResult<ArticuloModel>> GetArticuloModel(int id_articulo)
        {
            try
            {
                var articuloModel = await _context.Articulos
                    .Include(a => a.Precio)
                    .FirstOrDefaultAsync(a => a.Id_Articulo == id_articulo);

                if (articuloModel == null)
                {
                    Log.Information("El artículo con ID {IdArticulo} no fue encontrado.", id_articulo);
                    return NotFound("El artículo con ID {id_articulo} no fue encontrado.");
                }

                Log.Information("El artículo con ID {IdArticulo} fue encontrado con éxito.", id_articulo);
                return articuloModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Hubo un error al buscar el artículo con ID {IdArticulo}.", id_articulo);
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }

        // PUT: api/Articulo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id_Articulo}")]
        public async Task<IActionResult> PutArticuloModel(int id_Articulo, ArticuloModel articuloModel, [FromQuery] decimal precio)
        {
            try
            {
                var articulo = await _context.Articulos.FindAsync(id_Articulo);
                if (articulo == null)
                {
                    Log.Information("El artículo con ID {IdArticulo} no fue encontrado.", id_Articulo);
                    return NotFound("El artículo con ID {id_Articulo} no fue encontrado.");
                }
                articulo.Nombre_Articulo = articuloModel.Nombre_Articulo;
                articulo.Descripcion_Articulo = articuloModel.Descripcion_Articulo;
                articulo.Activo = articuloModel.Activo;
                articulo.Id_Categoria = articuloModel.Id_Categoria;

                if (precio > 0)
                {
                    var precioModel = await _context.Precios.FirstOrDefaultAsync(p => p.Id_Articulo == id_Articulo);
                    if (precioModel != null)
                    {
                        Log.Information("Se actualizo el precio del artículo con ID {IdArticulo} a {Precio}.", id_Articulo, precio);
                        precioModel.Precio = precio;
                        _context.Precios.Update(precioModel);
                    }
                    else
                    {
                        Log.Information("Creando un nuevo precio para el artículo con ID {IdArticulo} con el valor {Precio}.", id_Articulo, precio);
                        var newPrecio = new PrecioModel
                        {
                            Id_Articulo = id_Articulo,
                            Precio = precio
                        };
                        _context.Precios.Add(newPrecio);
                    }
                }

                _context.Articulos.Update(articulo);
                await _context.SaveChangesAsync();
                Log.Information("El artículo con ID {IdArticulo} se actualizó con exito.", id_Articulo);

                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Hubo un error al actualizar el artículo con ID {IdArticulo}.", id_Articulo);
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }




        // POST: api/Articulo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArticuloModel>> PostArticuloModel(ArticuloModel articuloModel, decimal precio)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {

                var categoriaExistente = await _context.Categorias
                                                       .FirstOrDefaultAsync(c => c.Id_Categoria == articuloModel.Id_Categoria);

                if (categoriaExistente == null)
                {
                    Log.Information("La categoría con ID {IdCategoria} no existe.", articuloModel.Id_Categoria);
                    return BadRequest("La categoría especificada no existe.");
                }

                _context.Articulos.Add(articuloModel);
                await _context.SaveChangesAsync();
                Log.Information("El artículo con ID {IdArticulo} se creó correctamente.", articuloModel.Id_Articulo);

                var precioModel = new PrecioModel
                {
                    Id_Articulo = articuloModel.Id_Articulo,
                    Precio = precio
                };
                _context.Precios.Add(precioModel);
                await _context.SaveChangesAsync();
                Log.Information("El precio {Precio} se agrego correctamente al artículo con ID {IdArticulo}.", precio, articuloModel.Id_Articulo);

                await transaction.CommitAsync();

                return CreatedAtAction("GetArticuloModel", new { id_articulo = articuloModel.Id_Articulo }, articuloModel);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Log.Error(ex, "Ocurrió un error al guardar el artículo y su precio.");
                return StatusCode(500, "Error al guardar el artículo y su precio.");
            }
        }

        // DELETE: api/Articulo/5
        [HttpDelete("{id_Articulo}")]
        public async Task<IActionResult> DeleteArticuloModel(int id_Articulo)
        {
            try
            {

                var articuloModel = await _context.Articulos.FindAsync(id_Articulo);
                if (articuloModel == null)
                {
                    Log.Information("No se encontró el artículo con ID {IdArticulo}.", id_Articulo);
                    return NotFound(new { message = $"El artículo con ID {id_Articulo} no fue encontrado." });
                }

                articuloModel.Activo = false;

                _context.Articulos.Update(articuloModel);
                await _context.SaveChangesAsync();
                Log.Information("El artículo con ID {IdArticulo} se borro corectamente.", id_Articulo);

                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ocurrió un error al realizar el borrado el artículo con ID {IdArticulo}.", id_Articulo);
                return StatusCode(500, "Error al borrar logicamente el artículo.");
            }
        }


        private bool ArticuloModelExists(int id_Articulo)
        {
            return _context.Articulos.Any(e => e.Id_Articulo == id_Articulo);
        }
    }
}
