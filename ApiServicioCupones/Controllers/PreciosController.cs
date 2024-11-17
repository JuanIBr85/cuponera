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
        [HttpPut("{id_Precio}")]
        public async Task<IActionResult> PutPrecioModel(int id_Precio, PrecioModel precioModel)
        {
            if (id_Precio != precioModel.Id_Precio)
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
                if (!PrecioModelExists(id_Precio))
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
        [HttpDelete("{id_Precio}")]
        public async Task<IActionResult> DeletePrecioModel(int id_Precio)
        {
            var precioModel = await _context.Precios
                .Include(p => p.Articulo)
                .FirstOrDefaultAsync(p => p.Id_Precio == id_Precio);

            if (precioModel == null)
            {
                return NotFound();
            }
            precioModel.Precio = 0;
            _context.Precios.Update(precioModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool PrecioModelExists(int id_Precio)
        {
            return _context.Precios.Any(e => e.Id_Precio == id_Precio);
        }
    }
}
