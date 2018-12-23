using System;
using Android.App;
using Android.Content;
using Android.Gms.Location;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;

namespace projekt_1.BroadcastRecievers
{
    public class GeofenceBroadcastReciever : BroadcastReceiver
    {
        private const string CHANNEL_NAME = "Geofence";
        private const string CHANNEL_ID = "345";

        private int _notificationId = 0;

        public override void OnReceive(Context context, Intent intent)
        {
            var geofencingEvent = GeofencingEvent.FromIntent(intent);
            var geofenceTransition = geofencingEvent.GeofenceTransition;
            var message = "";

            switch (geofenceTransition)
            {
                case Geofence.GeofenceTransitionEnter:
                    message = intent.GetStringExtra(Constans.EntryMessage);
                    break;
                case Geofence.GeofenceTransitionExit:
                    message = intent.GetStringExtra(Constans.ExitMessage);
                    break;
            }

            CreateNotificationChannel(context);
            SendNotificationToChannel(CHANNEL_ID, CHANNEL_NAME, Resource.Mipmap.ic_launcher,
                message, context, NotificationCompat.PriorityDefault, true);
        }

        private void CreateNotificationChannel(Context context)
        {
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.O)
            {
                var name = CHANNEL_NAME;
                var description = CHANNEL_NAME;
                var importance = NotificationManager.ImportanceMax;

                var channel = new NotificationChannel(CHANNEL_ID, name, importance);
                var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);

                notificationManager.CreateNotificationChannel(channel);
            }
        }

        public void SendNotificationToChannel(string channelId, string contentTitle, int resourceIcon, string contentText, Context context, int priority, bool closeOnClick)
        {
            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(context, channelId)
                .SetContentTitle(contentTitle)
                .SetSmallIcon(resourceIcon)
                .SetContentText(contentText)
                .SetPriority(priority)
                .SetAutoCancel(closeOnClick);

            NotificationManagerCompat nm = NotificationManagerCompat.From(context);
            nm.Notify(_notificationId++, notificationBuilder.Build());
        }
    }
}
