using Microsoft.AspNetCore.Mvc;
using Moq;
using NASA_Asteroids.Business.Interfaces;
using NASA_Asteroids.Controllers;

namespace NASA_Asteroids.Testing
{
    public class Endpoints
    {

        /// <summary>
        /// Validamos solo el Badrequest del controller
        /// </summary>
        /// <param name="days"></param>
        [Theory]
        [InlineData(-2)]
        [InlineData(0)]
        [InlineData(9)]
        public void Should_GetAsteroid_ReturnBadRequest(int days)
        {
            // Arrange

            //Mock de IExplorer
            IMock<IExplorer> businessEmplorer = new Mock<IExplorer>();
            //Mock de IAsteroids
            IMock<IAsteroids> businessAsteroids = new Mock<IAsteroids>();
            //Controller
            Controllers.AsteroidsController _asteroidControllerMock = new AsteroidsController(businessEmplorer.Object, businessAsteroids.Object);
            //Modelo input para el controler
            var model = new Business.DTO.ExplorerResultRequest() { Days = days };
            var controller = _asteroidControllerMock;
            controller.ModelState.AddModelError("days", "El parámetro 'days' debe ser un valor entre 1 y 7");

            // Act
            var result = controller.Get(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

    }
}
