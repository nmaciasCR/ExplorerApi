namespace NASA_Asteroids.Business
{
    public class Explorer : Interfaces.IExplorer
    {
        private Data.Entities.NASAContext _dbContext;
        private Services.NASA.Model.INasa _servicesNAZA;
        private Interfaces.IAsteroids _businessAsteroids;


        public Explorer(Data.Entities.NASAContext dbContext, Services.NASA.Model.INasa servicesNAZA, Interfaces.IAsteroids businessAsteroids)
        {
            _dbContext = dbContext;
            _servicesNAZA = servicesNAZA;
            _businessAsteroids = businessAsteroids;
        }

        /// <summary>
        /// Retorna los TOP Asteroides solicitados segun criterio
        /// 1- Con potencial de impacto
        /// 2- De mayor tamaño (de mas grande a mas chico)
        /// 3- Que orbiten a la TIERRA
        /// </summary>
        /// <param name="top">Cantidad de asteroides</param>
        /// <param name="dateFrom">Fecha de unicio</param>
        /// <param name="dateTo">Fecha Fin</param>
        /// <returns></returns>
        public async Task<List<Services.NASA.Model.AsteroidDetail>> GetTopAzardousAsteroids(int top, DateTime dateFrom, DateTime dateTo)
        {
            return await Task.Run(() =>
            {
                List<Services.NASA.Model.AsteroidDetail> topAsteroids;

                try
                {
                    //Pedimos los asteroides al servicio de la NASA
                    topAsteroids = _servicesNAZA.DetectObjects(dateFrom, dateTo);
                    //Filtramos segun requerimientos
                    return topAsteroids
                            .Where(a => a.is_potentially_hazardous_asteroid
                                        && a.orbiting_body == "Earth")
                            .OrderByDescending(ta => ta.estimated_diameter_avg_km)
                            .Take(top)
                            .ToList();
                }
                catch
                {
                    //log error
                    throw new Exception($"ERROR en el metodo GetTopAzardousAsteroids({top}, {dateFrom.ToString()}, {dateTo.ToString()})");
                }
            });
        }

        /// <summary>
        /// Guardamos la consulta en la DDBB
        /// </summary>
        /// <param name="dateEntry"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="asteroids"></param>
        public int Save(DateTime dateEntry, DateTime dateFrom, DateTime dateTo, List<Services.NASA.Model.AsteroidDetail> asteroids)
        {
            try
            {
                Data.Entities.Explorations newExplorer = new Data.Entities.Explorations();
                newExplorer.entryDate = dateEntry;
                newExplorer.dateFrom = dateFrom;
                newExplorer.dateTo = dateTo;
                newExplorer.asteroid = new List<Data.Entities.Asteroids>();
                //Si el asteroide ya existe lo referenciamos, sino lo creamos
                foreach (var ast in asteroids)
                {
                    Data.Entities.Asteroids? dbAsteroid = _businessAsteroids.GetAsteroid(ast.neo_reference_id);
                    if (dbAsteroid == null)
                    {
                        //Generamos un nuevo asteroide
                        newExplorer.asteroid.Add(new Data.Entities.Asteroids
                        {
                            neo_reference_id = ast.neo_reference_id,
                            name = ast.name,
                            estimated_diameter_min_km = ast.estimated_diameter_min_km,
                            estimated_diameter_max_km = ast.estimated_diameter_max_km,
                            is_potentially_hazardous_asteroid = ast.is_potentially_hazardous_asteroid,
                            close_approach_date = ast.close_approach_date,
                            relative_velocity_km_per_hour = ast.relative_velocity_km_per_hour,
                            miss_distance_km = ast.miss_distance_km,
                            orbiting_body = ast.orbiting_body
                        });
                    }
                    else
                    {
                        //referenciamos el asteroide existente
                        newExplorer.asteroid.Add(dbAsteroid);
                    }
                }

                //Guardamos la busqueda y resultado en la DDBB
                _dbContext.Explorations.Add(newExplorer);
                _dbContext.SaveChanges();

                return newExplorer.ID;


            }
            catch
            {
                //Log error
                return 0;
            }


        }


    }
}
