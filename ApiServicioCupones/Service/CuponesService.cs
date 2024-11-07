using ApiServicioCupones.Interfaces;

namespace ApiServicioCupones.Service
{
    public class CuponesService : ICuponesService
    {
        public async Task <string> GenerarNroCupon()
        {
            var random = new Random();

            var tresCifras1 = random.Next(0,1000).ToString("D3");
            var tresCifras2 = random.Next(0,1000).ToString("D3");
            var tresCifras3 = random.Next(0,1000).ToString("D3");

            var nroCupon = $"{tresCifras1}-{tresCifras2}-{tresCifras3}";

            return nroCupon;
        }
    }
}
