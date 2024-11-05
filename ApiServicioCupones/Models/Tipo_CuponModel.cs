using System.ComponentModel.DataAnnotations;

namespace ApiServicioCupones.Models
{
    public class Tipo_CuponModel
    {
        [Key]
        public int Id_Tipo_Cupon {  get; set; }
        public string Nombre { get; set; }
    }
}
