using System.ComponentModel.DataAnnotations;

namespace ApiServicioCupones.Models
{
    public class Cupon_DetalleModel
    {
        [Key]
        public int Id_Cupon { get; set; }
        [Key]
        public int Id_Articulo { get; set; }
        public int Cantidad {  get; set; }
    }
}
