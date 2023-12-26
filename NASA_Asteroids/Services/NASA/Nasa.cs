using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace NASA_Asteroids.Services.NASA
{
    public class Nasa : Model.INasa
    {

        const string URL_ASTEROIDS = @"https://api.nasa.gov/neo/rest/v1/feed";

        public Nasa()
        {

        }


        public List<Model.AsteroidDetail> DetectObjects(DateTime startDate, DateTime endDate)
        {
            List<Model.AsteroidDetail> listReturn = new List<Model.AsteroidDetail>();
            string start_date = startDate.ToString("yyyy-MM-dd");
            string end_date = endDate.ToString("yyyy-MM-dd");

            try
            {
                var url = URL_ASTEROIDS + $"?start_date={start_date}&end_date={end_date}&api_key=DEMO_KEY";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Accept = "application/json";


                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) throw new WebException($"ERROR EN {url}");
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            //Generamos listado de asteroides
                            JObject jResponse = JObject.Parse(responseBody);

                            if (jResponse != null)
                            {
                                JObject asteroidsPerDateArray = (JObject)jResponse["near_earth_objects"];
                                //Obtenemos los datos de cada asteroide
                                foreach (var property in asteroidsPerDateArray.Properties())
                                {
                                    JArray asteroidsArray = (JArray)property.Value;
                                    foreach (JObject asteroid in asteroidsArray)
                                    {
                                        try
                                        {
                                            listReturn.Add(new Model.AsteroidDetail
                                            {
                                                neo_reference_id = (string)asteroid["neo_reference_id"],
                                                name = (string)asteroid["name"],
                                                estimated_diameter_min_km = (double)asteroid["estimated_diameter"]["kilometers"]["estimated_diameter_min"],
                                                estimated_diameter_max_km = (double)asteroid["estimated_diameter"]["kilometers"]["estimated_diameter_max"],
                                                is_potentially_hazardous_asteroid = (bool)asteroid["is_potentially_hazardous_asteroid"],
                                                close_approach_date = DateTime.ParseExact((string)asteroid["close_approach_data"][0]["close_approach_date"], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                                                relative_velocity_km_per_hour = (double)asteroid["close_approach_data"][0]["relative_velocity"]["kilometers_per_hour"],
                                                miss_distance_km = (double)asteroid["close_approach_data"][0]["miss_distance"]["kilometers"],
                                                orbiting_body = (string)asteroid["close_approach_data"][0]["orbiting_body"]
                                            }) ;

                                        } catch { 
                                            //Log error al no poder parsear un asteroide
                                        }
                                    }
                                }


                            } else
                            {
                                throw new WebException("Ah ocurrido un error al obtener la respuesta del servicio de la NASA");
                            }

                        }
                    }
                }

                return listReturn;

            }
            catch (Exception ex)
            {
                //Log error
                throw new Exception(ex.Message);
            }

        }


    }
}
