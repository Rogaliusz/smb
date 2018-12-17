using System;
using Plugin.Geofence;
using Plugin.Geofence.Abstractions;

namespace projekt_1.Services.Geolocation
{
    public interface IGeofenceService : IService
    {
        void SetGeofenceTrigger(double lat, double lng, double radius,
            string entryMessage, string exitMessage, string name);
    }
}
