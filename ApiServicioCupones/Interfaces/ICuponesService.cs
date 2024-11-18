namespace ApiServicioCupones.Interfaces
{
    public interface ICuponesService
    {
        Task ActivarCuponPorFecha();
        Task DesactivaCuponPorFecha();
        Task <string> GenerarNroCupon();
    }
}
