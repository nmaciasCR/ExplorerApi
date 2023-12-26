namespace NASA_Asteroids.Business.Interfaces
{
    public interface IExplorer
    {

        List<Services.NASA.Model.AsteroidDetail> GetTopAzardousAsteroids(int top, DateTime dateFrom, DateTime dateTo);
    }
}
