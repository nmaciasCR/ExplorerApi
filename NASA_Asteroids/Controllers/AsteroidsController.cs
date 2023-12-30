using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NASA_Asteroids.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AsteroidsController : ControllerBase
    {
        private Business.Interfaces.IExplorer _busimessExplorer;
        private Business.Interfaces.IAsteroids _businessAsteroids;

        public AsteroidsController(Business.Interfaces.IExplorer busimessExplorer,
                                   Business.Interfaces.IAsteroids businessAsteroids)
        {
            _busimessExplorer = busimessExplorer;
            _businessAsteroids = businessAsteroids;
        }

        [HttpGet]
        public ActionResult Get([FromQuery] Business.DTO.ExplorerResultRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            const int TOP = 3;
            DateTime dateRequest = DateTime.Now;
            DateTime dateFrom = DateTime.Now.Date;
            DateTime dateTo = dateFrom.AddDays(model.Days).Date;
            Business.DTO.ExplorerResultResponse explorerResult;

            try
            {
                //TOP asteroides
                List<Services.NASA.Model.AsteroidDetail> asteroids = _busimessExplorer.GetTopAzardousAsteroids(TOP, dateFrom, dateTo);
                //Guardamos la consulta en la DDBB
                int trackId = _busimessExplorer.Save(dateRequest, dateFrom, dateTo, asteroids);
                //Creamos el objeto de respuesta
                explorerResult = new Business.DTO.ExplorerResultResponse();
                explorerResult.trackId = trackId;
                explorerResult.fecha_ejecucion = dateRequest.ToString("yyyy-MM-dd HH:mm:ss");
                explorerResult.fecha_desde = dateFrom.ToString("yyyy-MM-dd");
                explorerResult.fecha_hasta = dateTo.ToString("yyyy-MM-dd");
                explorerResult.top = TOP;
                explorerResult.asteroides = new List<Business.DTO.Asteroids>(asteroids.Select(a => _businessAsteroids.MapToDTO(a)));

                return StatusCode(StatusCodes.Status200OK, explorerResult);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }



        }
    }
}
