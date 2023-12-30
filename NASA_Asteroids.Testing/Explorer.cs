using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASA_Asteroids.Testing
{
    public class Explorer
    {

        [Fact]
        public void Should_GetTopAzardousAsteroids_ReturnAsteroidDetailList()
        {
            // Arrange
            int TOP = 3;
            DateTime dateFrom = DateTime.Now.Date;
            DateTime dateTo = DateTime.Now.AddDays(3).Date;
            Mock<Data.Entities.NASAContext> _dbContextMock = new Mock<Data.Entities.NASAContext>();
            Mock<Services.NASA.Model.INasa> _serviceNAZAMock = new Mock<Services.NASA.Model.INasa>();
            Mock<Business.Interfaces.IAsteroids> _businessAsteroidsMock = new Mock<Business.Interfaces.IAsteroids>();
            Business.Interfaces.IExplorer _businessExplorer = new Business.Explorer(_dbContextMock.Object, _serviceNAZAMock.Object, _businessAsteroidsMock.Object);

            _serviceNAZAMock.Setup(s => s.DetectObjects(dateFrom, dateTo))
                            .Returns(new List<Services.NASA.Model.AsteroidDetail>
                            {
                                new Services.NASA.Model.AsteroidDetail()
                                {
                                    neo_reference_id = "123456",
                                    name = "name",
                                    estimated_diameter_min_km = 10,
                                    estimated_diameter_max_km = 20,
                                    is_potentially_hazardous_asteroid = true,
                                    close_approach_date = dateFrom,
                                    relative_velocity_km_per_hour = 1000,
                                    miss_distance_km = 15000,
                                    orbiting_body = "Earth"
                                }
                            });

            // Act
            List<Services.NASA.Model.AsteroidDetail> listReturn = _businessExplorer.GetTopAzardousAsteroids(TOP, dateFrom, dateTo);

            // Assert
            Assert.IsType<List<Services.NASA.Model.AsteroidDetail>>(listReturn);
        }





    }
}
