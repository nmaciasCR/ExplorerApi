namespace NASA_Asteroids.Business.Interfaces
{
    public interface IExplorer
    {
        List<Services.NASA.Model.AsteroidDetail> GetTopAzardousAsteroids(int top, DateTime dateFrom, DateTime dateTo);
        int Save(DateTime dateEntry, DateTime dateFrom, DateTime dateTo, List<Services.NASA.Model.AsteroidDetail> asteroids);
    }
}
