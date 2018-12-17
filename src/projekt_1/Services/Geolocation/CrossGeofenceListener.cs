using System;
using System.Diagnostics;
using Plugin.Geofence;
using Plugin.Geofence.Abstractions;

namespace projekt_1.Services.Geolocation
{
    public class CrossGeofenceListener : IGeofenceListener
    {
        private readonly string _tag = typeof(CrossGeofenceListener).Name;

        public void OnMonitoringStarted(string region)
        {
            Debug.WriteLine(string.Format("{0} - Monitoring started in region: {1}", _tag, region));
        }

        public void OnMonitoringStopped()
        {
            Debug.WriteLine(string.Format("{0} - {1}", _tag, "Monitoring stopped for all regions"));
        }

        public void OnMonitoringStopped(string identifier)
        {
            Debug.WriteLine(string.Format("{0} - {1}: {2}", _tag, "Monitoring stopped in region", identifier));
        }

        public void OnError(string error)
        {
            Debug.WriteLine(string.Format("{0} - {1}: {2}", _tag, "Error", error));
        }

        // Note that you must call CrossGeofence.GeofenceListener.OnAppStarted() from your app when you want this method to run.
        public void OnAppStarted()
        {
            Debug.WriteLine(string.Format("{0} - {1}", _tag, "App started"));
        }

        public void OnRegionStateChanged(GeofenceResult result)
        {
            Debug.WriteLine(string.Format("{0} - {1}", _tag, result.ToString()));
        }

        public void OnLocationChanged(GeofenceLocation location)
        {
            Debug.WriteLine(string.Format("{0} - {1} LAT:{2} LNG:{3} ", _tag, location.ToString(), location.Latitude, location.Longitude));
        }
    }
}
