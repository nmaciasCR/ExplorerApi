namespace NASA_Asteroids.Business
{
    public class Explorer : Interfaces.IExplorer
    {
        private Services.NASA.Model.INasa _servicesNAZA;


        public Explorer(Services.NASA.Model.INasa servicesNAZA)
        {
            _servicesNAZA = servicesNAZA;
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
        public List<Services.NASA.Model.AsteroidDetail> GetTopAzardousAsteroids(int top, DateTime dateFrom, DateTime dateTo)
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
                        .OrderByDescending(ta => ta.estimated_diameter_max_km)
                        .Take(top)
                        .ToList();



            } catch
            {
                //log error
                throw new Exception($"ERROR en el metodo GetTopAzardousAsteroids({top}, {dateFrom.ToString()}, {dateTo.ToString()})"); 
            }

        }


    }
}
