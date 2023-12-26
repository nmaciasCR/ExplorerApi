namespace NASA_Asteroids.Services.NASA.Model
{
    public interface INasa
    {
        List<Model.AsteroidDetail> DetectObjects(DateTime startDate, DateTime endDate);
    }
}
