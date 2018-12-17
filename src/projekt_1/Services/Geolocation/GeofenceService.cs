using System;
using Plugin.Geofence.Abstractions;

namespace projekt_1.Services.Geolocation
{
    public class GeofenceService : IGeofenceService
    {
        private readonly IGeofence _geofence;

        public GeofenceService(IGeofence geofence)
        {
            _geofence = geofence;
        }


        public void SetGeofenceTrigger(double lat, double lng, double radius,
            string entryMessage, string exitMessage, string name)
        {
            var region = new GeofenceCircularRegion(
                name,
                lat,
                lng,
                radius)
            {
                ShowExitNotification = true,
                NotificationExitMessage = exitMessage,
                ShowEntryNotification = true,
                NotificationEntryMessage = entryMessage
            };

            region.Radius = radius;

            _geofence.StartMonitoring(region);
        }
    }
}
