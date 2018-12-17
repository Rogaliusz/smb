using System;

namespace projekt_1.Models
{
    public class Shop
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public int Radius { get; set; } = 1000;
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}