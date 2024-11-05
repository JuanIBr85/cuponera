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
    public class Cupon_DetalleController : ControllerBase
    {
        private readonly DataBaseContext _context;

        public Cupon_DetalleController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: api/Cupon_Detalle
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cupon_DetalleModel>>> GetCupones_Detalle()
        {
            return await _context.Cupones_Detalle.ToListAsync();
        }

        // GET: api/Cupon_Detalle/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Cupon_DetalleModel>>> GetCupon_DetalleModel(int id)
        {
            var cupon_DetalleModel = await _context.Cupones_Detalle.Where(cd => cd.Id_Cupon == id).ToListAsync();

            if (cupon_DetalleModel == null)
            {
                return NotFound();
            }

            return cupon_DetalleModel;
        }

        // PUT: api/Cupon_Detalle/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCupon_DetalleModel(int id, Cupon_DetalleModel cupon_DetalleModel)
        {
            if (id != cupon_DetalleModel.Id_Cupon)
            {
                return BadRequest();
            }

            _context.Entry(cupon_DetalleModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Cupon_DetalleModelExists(id))
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

        // POST: api/Cupon_Detalle
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cupon_DetalleModel>> PostCupon_DetalleModel(Cupon_DetalleModel cupon_DetalleModel)
        {
            _context.Cupones_Detalle.Add(cupon_DetalleModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Cupon_DetalleModelExists(cupon_DetalleModel.Id_Cupon))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCupon_DetalleModel", new { id = cupon_DetalleModel.Id_Cupon }, cupon_DetalleModel);
        }

        // DELETE: api/Cupon_Detalle/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupon_DetalleModel(int id)
        {
            var cupon_DetalleModel = await _context.Cupones_Detalle.FindAsync(id);
            if (cupon_DetalleModel == null)
            {
                return NotFound();
            }

            _context.Cupones_Detalle.Remove(cupon_DetalleModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Cupon_DetalleModelExists(int id)
        {
            return _context.Cupones_Detalle.Any(e => e.Id_Cupon == id);
        }
    }
}
