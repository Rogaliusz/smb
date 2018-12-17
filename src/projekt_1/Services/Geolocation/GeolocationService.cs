using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Plugin.Geofence;
using Plugin.Geolocator;
using projekt_1.Framework;

namespace projekt_1.Services.Geolocation
{
    public class GeolocationService : IGeolocationService
    {
        public GeolocationService()
        {
        }

        public async Task StartListeningGeolocationAsync()
        {
            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(10), 1.0);
        }

        public async Task StopListeningGeolocationAsync()
        {
            await CrossGeolocator.Current.StopListeningAsync();
        }

        public async Task<IGeopoint> GetCurrentGeolocationAsync()
        {
            var loc = await CrossGeolocator.Current.GetPositionAsync();
            return new Geopoint(loc.Latitude, loc.Longitude);
        }
    }
}
