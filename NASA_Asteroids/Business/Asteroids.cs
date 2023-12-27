namespace NASA_Asteroids.Business
{
    public class Asteroids : Interfaces.IAsteroids
    {
        private Data.Entities.NASAContext _dbContext;

        public Asteroids(Data.Entities.NASAContext dbContext) {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Mapea un objeto asteroide del servicio de NADA en su correspondiente DTO
        /// </summary>
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

        /// <summary>
        /// Retorna un objeto ASTEROID de la DDBB
        /// </summary>
        /// <param name="neo_reference_id"></param>
        /// <returns></returns>
        public Data.Entities.Asteroids? GetAsteroid(string neo_reference_id)
        {
            return _dbContext.Asteroids.FirstOrDefault(a => a.neo_reference_id == neo_reference_id);
        }
    }
}
