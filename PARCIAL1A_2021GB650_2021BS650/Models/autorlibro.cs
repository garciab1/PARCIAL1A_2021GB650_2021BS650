using System.ComponentModel.DataAnnotations;

namespace PARCIAL1A_2021GB650_2021BS650.Models
{
    public class autorlibro
    {
        [Key]
        public int  AutorId { get; set; }
        public int? LibroId { get; set; }
        public int? Orden {  get; set; }
    }
}
