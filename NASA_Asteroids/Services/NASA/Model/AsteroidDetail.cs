namespace NASA_Asteroids.Services.NASA.Model
{
    public class AsteroidDetail
    {
        public string? neo_reference_id { get; set; }
        public string? name { get; set; }
        public double estimated_diameter_min_km { get; set; }
        public double estimated_diameter_max_km { get; set; }
        public bool is_potentially_hazardous_asteroid { get; set; }
        public DateTime close_approach_date { get; set; }
        public double relative_velocity_km_per_hour { get; set; }
        public double miss_distance_km { get; set; }
        public string? orbiting_body { get; set; }
    }
}
