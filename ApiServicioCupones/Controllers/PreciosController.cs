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
            return await _context.Precios.ToListAsync();
        }

        // GET: api/Precios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrecioModel>> GetPrecioModel(int id)
        {
            var precioModel = await _context.Precios.FindAsync(id);

            if (precioModel == null)
            {
                return NotFound();
            }

            return precioModel;
        }

        // PUT: api/Precios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrecioModel(int id, PrecioModel precioModel)
        {
            if (id != precioModel.Id_Precio)
            {
                return BadRequest();
            }

            _context.Entry(precioModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrecioModelExists(id))
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

        // POST: api/Precios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PrecioModel>> PostPrecioModel(PrecioModel precioModel)
        {
            _context.Precios.Add(precioModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrecioModel", new { id = precioModel.Id_Precio }, precioModel);
        }

        // DELETE: api/Precios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrecioModel(int id)
        {
            var precioModel = await _context.Precios.FindAsync(id);
            if (precioModel == null)
            {
                return NotFound();
            }

            _context.Precios.Remove(precioModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrecioModelExists(int id)
        {
            return _context.Precios.Any(e => e.Id_Precio == id);
        }
    }
}
