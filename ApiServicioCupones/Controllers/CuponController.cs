using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiServicioCupones.Data;
using ApiServicioCupones.Models;
using ApiServicioCupones.Service;
using ApiServicioCupones.Interfaces;
using Serilog;

namespace ApiServicioCupones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuponController : ControllerBase
    {
        private readonly DataBaseContext _context;
        private readonly ICuponesService _cuponesService;

        public CuponController(DataBaseContext context, ICuponesService cuponesService)
        {
            _context = context;
            _cuponesService = cuponesService;
        }

        // GET: api/Cupon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuponModel>>> GetCupones()
        {
            try
            {
                var cupones = await _context
                    .Cupones
                    .Include(c => c.Cupones_Categorias)
                        .ThenInclude(cc => cc.Categoria)
                    .Include(c => c.Tipo_Cupon)
                    .ToListAsync();

                Log.Information("Se obtuvieron {Count} cupones con éxito.", cupones.Count);

                return Ok(cupones);
            }
            catch (Exception ex)
            {
                Log.Error($"Hubo un problema al obtener los cupones, error: {ex.Message}");
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }


        // GET: api/Cupon/5
        [HttpGet("{id_cupon}")]
        public async Task<ActionResult<CuponModel>> GetCuponModel(int id_cupon)
        {
            try
            {
                var cuponModel = await _context.Cupones.FindAsync(id_cupon);

                if (cuponModel == null)
                {
                    Log.Information("No se encontró el cupón con ID {IdCupon}.", id_cupon);
                    return NotFound(new { message = $"El cupón con ID {id_cupon} no fue encontrado." });
                }

                Log.Information("Se obtuvo el cupón con ID {IdCupon}.", id_cupon);
                return cuponModel;
            }
            catch (Exception ex)
            {
                Log.Error($"Hubo un problema al obtener el cupón con ID {id_cupon}, error: {ex.Message}");
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }

        // PUT: api/Cupon/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id_cupon}")]
        public async Task<IActionResult> PutCuponModel(int id_cupon, CuponModel cuponModel)
        {
            if (id_cupon != cuponModel.Id_Cupon)
            {
                Log.Information("El ID del cupón no coincide con el ID proporcionado en el cuerpo ({ModelId}).", id_cupon, cuponModel.Id_Cupon);
                return BadRequest(new { message = "El ID del cupón no coincide." });
            }

            _context.Entry(cuponModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Log.Information("El cupón con ID {IdCupon} se ha actualizado correctamente.", id_cupon);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CuponModelExists(id_cupon))
                {
                    Log.Information("El cupón con ID {IdCupon} no se encontró durante la actualización.", id_cupon);
                    return NotFound(new { message = $"El cupón con ID {id_cupon} no fue encontrado." });
                }
                else
                {
                    Log.Error($"Hubo un problema al actualizar el cupón con ID {id_cupon}, error: {ex.Message}");
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Cupon
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CuponModel>> PostCuponModel(CuponModel cuponModel)
        {
            try
            {
                _context.Cupones.Add(cuponModel);
                await _context.SaveChangesAsync();
                Log.Information("El cupón con ID {IdCupon} se ha creado exitosamente.", cuponModel.Id_Cupon);

                return CreatedAtAction("GetCuponModel", new { id_cupon = cuponModel.Id_Cupon }, cuponModel);
            }
            catch (Exception ex)
            {
                Log.Error($"Hubo un problema al crear el cupón, error: {ex.Message}");
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }


        [HttpPost("Desactiva-cupones")]
        public async Task<IActionResult> DesactivarCupones()
        {
            try
            {
                await _cuponesService.DesactivaCuponPorFecha();

                Log.Information("Cupones expirados desactivados correctamente.");

                return Ok("Cupones vencidos desactivados correctamente.");
            }
            catch (Exception ex)
            {
                Log.Error($"Hubo un problema al desactivar los cupones expirados, error: {ex.Message}");
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }

        [HttpPost("Activa-cupones")]
        public async Task<IActionResult> ActivarCupones()
        {
            try
            {
                await _cuponesService.ActivarCuponPorFecha();

                Log.Information("Cupones activados correctamente.");

                return Ok("Cupones activados correctamente.");
            }
            catch (Exception ex)
            {
                Log.Error($"Hubo un problema al activar los cupones, error: {ex.Message}");
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }


        // DELETE: api/Cupon/5
        [HttpDelete("{id_cupon}")]
        public async Task<IActionResult> DeleteCuponModel(int id_cupon)
        {
            try
            {
                var cuponModel = await _context.Cupones.FindAsync(id_cupon);
                if (cuponModel == null)
                {
                    Log.Information($"El cupón con ID {id_cupon} no fue encontrado.");
                    return NotFound();
                }

                // Desactivar el cupón en lugar de eliminarlo
                cuponModel.Activo = false;

                _context.Cupones.Update(cuponModel);
                await _context.SaveChangesAsync();

                Log.Information($"El cupón con ID {id_cupon} ha sido desactivado correctamente.");
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error($"Hubo un problema al desactivar el cupón con ID {id_cupon}, error: {ex.Message}");
                return BadRequest($"Hubo un problema, error: {ex.Message}");
            }
        }


        private bool CuponModelExists(int id_cupon)
        {
            return _context.Cupones.Any(e => e.Id_Cupon == id_cupon);
        }
    }
}
