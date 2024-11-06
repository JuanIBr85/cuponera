using ApiServicioCupones.Interfaces;

namespace ApiServicioCupones.Service
{
    public class CuponesService : ICuponesService
    {
        public async Task <string> GenerarNroCupon()
        {
            /* Logica para la creacion del cupon aleatorio 123-456-789
             * 
             *
             */

            var nroCupon = "123-456-789";

            return nroCupon;
        }
    }
}
