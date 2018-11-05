using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace projekt_2.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private static int _notificationId = 0;

        private readonly Context _context;

        public NotificationService(Context context)
        {
            _context = context;
        }

        public void CreateNotificationChannel(string channelId, string channelName)
        {

            if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.O) return;

            NotificationManager notificationManager = (NotificationManager)_context.GetSystemService(Context.NotificationService);

            var channel = notificationManager.GetNotificationChannel(channelId);
            if (channel != null) return;

            NotificationChannel notificationChannel = new NotificationChannel(channelId, channelName, NotificationManager.ImportanceLow);
            notificationChannel.EnableLights(true);
            notificationChannel.LightColor = Color.Red;
            notificationChannel.EnableVibration(true);
            notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });

            notificationManager.CreateNotificationChannel(notificationChannel);
        }

        public void SendNotificationToChannel(string channelId, string contentTitle, int resourceIcon, string contentText, PendingIntent pendingIntent, int priority, bool closeOnClick)
        {
            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(_context, channelId)
                .SetContentTitle(contentTitle)
                .SetSmallIcon(resourceIcon)
                .SetContentText(contentText)
                .SetContentIntent(pendingIntent)
                .SetPriority(priority)
                .SetAutoCancel(closeOnClick);

            NotificationManagerCompat nm = NotificationManagerCompat.From(_context);
            nm.Notify(_notificationId++, notificationBuilder.Build());
        }
    }
}