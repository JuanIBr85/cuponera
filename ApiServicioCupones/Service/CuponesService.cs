using ApiServicioCupones.Data;
using ApiServicioCupones.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ApiServicioCupones.Service
{
    public class CuponesService : ICuponesService
    {
        private readonly DataBaseContext _context;

        public CuponesService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<string> GenerarNroCupon()
        {

            var random = new Random();

            string nroCupon;
            do
            {

                var tresCifras1 = random.Next(0, 1000).ToString("D3");
                var tresCifras2 = random.Next(0, 1000).ToString("D3");
                var tresCifras3 = random.Next(0, 1000).ToString("D3");

                nroCupon = $"{tresCifras1}-{tresCifras2}-{tresCifras3}";

            } while (await _context.Cupones_Clientes.AnyAsync(c => c.NroCupon == nroCupon));

            return nroCupon;
        }

        public async Task DesactivaCuponPorFecha()
        {

            var cuponesActivos = await _context.Cupones
                    .Where(c=> c.Activo == true)
                    .ToListAsync();

            foreach (var cupon in cuponesActivos)
            {
                var fechaActual = DateOnly.FromDateTime(DateTime.Now);

                if(fechaActual<cupon.FechaInicio || fechaActual > cupon.FechaFin) 
                {
                    cupon.Activo = false;
                    _context.Cupones.Update(cupon);
                }
            }

            await _context.SaveChangesAsync();
            
        }
        public async Task ActivarCuponPorFecha()
        {

            var cuponesDescativados = await _context.Cupones
                    .Where(c => c.Activo == false)
                    .ToListAsync();

            foreach (var cupon in cuponesDescativados)
            {
                var fechaActual = DateOnly.FromDateTime(DateTime.Now);

                if (fechaActual >= cupon.FechaInicio && fechaActual <= cupon.FechaFin)
                {
                    cupon.Activo = true;
                    _context.Cupones.Update(cupon);
                }
            }

            await _context.SaveChangesAsync();

        }
    }
}
