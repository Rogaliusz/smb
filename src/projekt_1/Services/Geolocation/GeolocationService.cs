using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Plugin.Geolocator;
using projekt_1.Framework;

namespace projekt_1.Services.Geolocation
{
    public class GeolocationService : IGeolocationService
    {
        public GeolocationService()
        {
        }

        public async Task<IGeopoint> GetCurrentGeolocationAsync()
        {
            var loc = await CrossGeolocator.Current.GetPositionAsync();
            return new Geopoint(loc.Latitude, loc.Longitude);
        }
    }
}
