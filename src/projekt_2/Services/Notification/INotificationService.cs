using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace projekt_2.Services.Notification
{
    public interface INotificationService
    {
        void CreateNotificationChannel(string channelId, string channelName);
        void SendNotificationToChannel(string channelId, string contentTitle, int resourceIcon, string contentText, PendingIntent pendingIntent, int priority, bool closeOnClick);
    }
}