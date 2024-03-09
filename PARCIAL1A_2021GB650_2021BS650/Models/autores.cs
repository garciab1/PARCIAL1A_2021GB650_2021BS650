using System.ComponentModel.DataAnnotations;

namespace PARCIAL1A_2021GB650_2021BS650.Models
{
    public class autores
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre {get; set;}

    }
}
