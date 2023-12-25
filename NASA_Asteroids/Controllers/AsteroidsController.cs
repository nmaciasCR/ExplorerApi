using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NASA_Asteroids.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AsteroidsController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get([Required(ErrorMessage = "El parámetro 'days' es obligatorio")]
                                [Range(1, 7, ErrorMessage = "El parámetro 'days' debe ser un valor entre 1 y 7")]
                                int? days)
        {
            return StatusCode(StatusCodes.Status200OK, days);
        }
    }
}
