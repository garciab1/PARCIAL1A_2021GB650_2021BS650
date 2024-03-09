namespace PARCIAL1A_2021GB650_2021BS650.Models
{
    public class posts
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Contenido { get; set; }
        public  DateTime? FechaPublicacion { get; set; }
        public int? AutorId { get; set; }
    }
}
