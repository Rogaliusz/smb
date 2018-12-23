using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Android.App;
using Android.Content;
using Android.Gms.Location;
using Android.Gms.Tasks;
using Java.Lang;

namespace projekt_1.Services.Geolocation
{
    public class GeofenceService : Java.Lang.Object, IGeofenceService , IOnSuccessListener, IOnFailureListener, IOnCompleteListener
    {
        private readonly Context _context;
        private readonly GeofencingClient _geofenceClient;
        private readonly IList<IGeofence> _regions;

        private int requestId = 0;

        public IntPtr Handle { get;}

        public GeofenceService()
        {

        }

        public GeofenceService(Context context)
        {
            _context = context;
            _geofenceClient = LocationServices.GetGeofencingClient(context);
            _regions = new List<IGeofence>();
        }

        public async void SetGeofenceTrigger(double lat, double lng, double radius,
            string entryMessage, string exitMessage, string name)
        {
            var builder = new GeofenceBuilder();
            var instance = builder.SetRequestId(name)
                .SetCircularRegion(lat, lng, (float) radius)
                .SetExpirationDuration(Geofence.NeverExpire)
                .SetTransitionTypes(Geofence.GeofenceTransitionEnter | Geofence.GeofenceTransitionExit)
                .Build();

            _regions.Add(instance);

            var request = CreateGeofencingRequest(instance);
            var pendingIntent = CreatePendintIntent(entryMessage, exitMessage);

            await _geofenceClient.AddGeofencesAsync(request, pendingIntent);

        }

        private PendingIntent CreatePendintIntent(string entryMessage, string exitMessage)
        {
            Intent intent = new Intent();
            intent.SetAction(Constans.GeofenceIntent);
            intent.PutExtra(Constans.ExitMessage, exitMessage);
            intent.PutExtra(Constans.EntryMessage, entryMessage);

            var pendingIntent = PendingIntent.GetBroadcast(_context, requestId++, intent, PendingIntentFlags.UpdateCurrent);
            return pendingIntent;
        }

        private GeofencingRequest CreateGeofencingRequest(IGeofence geofence)
        {
            var builder = new GeofencingRequest.Builder();
            builder.SetInitialTrigger(GeofencingRequest.InitialTriggerEnter);
            builder.AddGeofence(geofence);
            return builder.Build();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            Debug.Print("Yes");
        }

        public void Dispose()
        {

        }

        public void OnFailure(Java.Lang.Exception e)
        {
            Debug.Print("Fucked");
        }

        public void OnComplete(Task task)
        {
            Debug.Print("Fucked");
        }
    }
}
