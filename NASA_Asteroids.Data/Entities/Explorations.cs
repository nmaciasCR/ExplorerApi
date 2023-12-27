using System;
using System.Collections.Generic;

namespace NASA_Asteroids.Data.Entities;

public partial class Explorations
{
    public int ID { get; set; }

    public DateTime entryDate { get; set; }

    public DateTime dateFrom { get; set; }

    public DateTime dateTo { get; set; }

    public virtual ICollection<Asteroids> asteroid { get; set; } = new List<Asteroids>();
}
