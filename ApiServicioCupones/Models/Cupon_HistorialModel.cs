using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace ApiServicioCupones.Models
{
    public class Cupon_HistorialModel
    {
        [Key]
        public int Id_Cupon { get; set; }
        [Key]
        public string NroCupon { get; set; }
        public DateOnly FechaUso { get; set; }
        public string CodCliente {  get; set; }
    }
}
