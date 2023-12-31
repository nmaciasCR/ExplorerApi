﻿namespace NASA_Asteroids.Business.DTO
{
    public class ExplorerResultResponse
    {
        public int trackId { get; set; }
        public string fecha_ejecucion { get; set; }
        public string fecha_desde { get; set; }
        public string fecha_hasta { get; set; }
        public int top { get; set; }
        public List<Asteroids> asteroides { get; set; }
    }
}
