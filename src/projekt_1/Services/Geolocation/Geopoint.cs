using System;
namespace projekt_1.Services.Geolocation
{
    public interface IGeopoint
    { 
        double Latitude { get; }
        double Longitude { get; }
    }

    public class Geopoint : IGeopoint
    {
        public Geopoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
    }
}
