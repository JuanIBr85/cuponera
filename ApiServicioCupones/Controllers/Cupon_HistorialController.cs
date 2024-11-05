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
    public class Cupon_HistorialController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public Cupon_HistorialController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Cupon_Historial
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cupon_HistorialModel>>> GetCupones_Historial()
        {
            return await _context.Cupones_Historial.ToListAsync();
        }

        // GET: api/Cupon_Historial/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cupon_HistorialModel>> GetCupon_HistorialModel(int id)
        {
            var cupon_HistorialModel = await _context.Cupones_Historial.FindAsync(id);

            if (cupon_HistorialModel == null)
            {
                return NotFound();
            }

            return cupon_HistorialModel;
        }

        // PUT: api/Cupon_Historial/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCupon_HistorialModel(int id, Cupon_HistorialModel cupon_HistorialModel)
        {
            if (id != cupon_HistorialModel.Id_Cupon)
            {
                return BadRequest();
            }

            _context.Entry(cupon_HistorialModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cupon_HistorialModelExists(id))
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

        // POST: api/Cupon_Historial
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cupon_HistorialModel>> PostCupon_HistorialModel(Cupon_HistorialModel cupon_HistorialModel)
        {
            _context.Cupones_Historial.Add(cupon_HistorialModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Cupon_HistorialModelExists(cupon_HistorialModel.Id_Cupon))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCupon_HistorialModel", new { id = cupon_HistorialModel.Id_Cupon }, cupon_HistorialModel);
        }

        // DELETE: api/Cupon_Historial/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupon_HistorialModel(int id)
        {
            var cupon_HistorialModel = await _context.Cupones_Historial.FindAsync(id);
            if (cupon_HistorialModel == null)
            {
                return NotFound();
            }

            _context.Cupones_Historial.Remove(cupon_HistorialModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Cupon_HistorialModelExists(int id)
        {
            return _context.Cupones_Historial.Any(e => e.Id_Cupon == id);
        }
    }
}
