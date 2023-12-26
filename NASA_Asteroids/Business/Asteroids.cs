namespace NASA_Asteroids.Business
{
    public class Asteroids : Interfaces.IAsteroids
    {
        public Asteroids() { }

        public DTO.Asteroids MapToDTO(Services.NASA.Model.AsteroidDetail ast)
        {
            return new DTO.Asteroids()
            {
                nombre = ast.name,
                diametro = ast.estimated_diameter_avg_km,
                velocidad = ast.relative_velocity_km_per_hour,
                fecha = ast.close_approach_date.ToString("yyyy-MM-dd"),
                planeta = ast.orbiting_body
            };
        }
    }
}
