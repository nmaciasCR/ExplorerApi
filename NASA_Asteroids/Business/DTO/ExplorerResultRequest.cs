using System.ComponentModel.DataAnnotations;

namespace NASA_Asteroids.Business.DTO
{
    public class ExplorerResultRequest
    {
        [Required(ErrorMessage = "El parámetro 'days' es obligatorio")]
        [Range(1, 7, ErrorMessage = "El parámetro 'days' debe ser un valor entre 1 y 7")]
        public int Days { get; set; }
    }
}
