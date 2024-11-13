namespace ApiServicioCupones.Models.DTO
{
    public class ClienteDto {

        public int id_Cupon { get; set; }
        public string CodCliente { get; set; }
        public string NroCupon { get; set; }
        public DateTime FechaAsignado { get; set; }


         public string Email { get; set; }
    }

}