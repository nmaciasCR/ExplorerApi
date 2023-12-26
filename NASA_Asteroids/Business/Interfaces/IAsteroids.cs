namespace NASA_Asteroids.Business.Interfaces
{
    public interface IAsteroids
    {
        DTO.Asteroids MapToDTO(Services.NASA.Model.AsteroidDetail ast);
    }
}
