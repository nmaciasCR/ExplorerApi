using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASA_Asteroids.Testing
{
    public class NASA_Service
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void Should_DetectObjects_ReturnAsteroidDetail(int days)
        {
            // Arrange
            Services.NASA.Model.INasa NASAService = new Services.NASA.Nasa();
            DateTime dateFrom = DateTime.Now.Date;
            DateTime dateTo = dateFrom.AddDays(days).Date;

            // Act
            List<Services.NASA.Model.AsteroidDetail> listServiceReturn = NASAService.DetectObjects(dateFrom, dateTo);

            // Assert
            Assert.IsType<List<Services.NASA.Model.AsteroidDetail>>(listServiceReturn);
        }




    }
}
