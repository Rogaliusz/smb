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
using common.Models;
using common.Permissions;
using common.Services.Json;

namespace projekt_2.BroadcastRecievers
{
    public class ProductCreatedBroadcastReciever : BaseBroadcastReciever
    {
        private static int _notificationId = 0;
        private static string _channelName = "channel1";
        private static string _channelId = "666";

        private readonly IJsonService _jsonService;

        public ProductCreatedBroadcastReciever()
        {
            _jsonService = IoC.Container.GetInstance<IJsonService>();
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var serializedModel = intent.GetStringExtra(common.Extras.PRODUCT);
            var model = _jsonService.Deserialize<Product>(serializedModel);

            var notificationClickedIntent = new Intent(common.Intents.GO_TO_PRODUCT_EDIT);
            notificationClickedIntent.PutExtra(common.Extras.ID, model.Id);
            var pendingIntent = PendingIntent.GetActivity(context, 0, notificationClickedIntent, 0);

            CreateNotificationChannel(context);
            CreateNotification(context, model, pendingIntent);
        }

        private static void CreateNotification(Context context, Product model, PendingIntent pendingIntent)
        {
            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(context, _channelId)
                .SetContentTitle(context.GetString(Resource.String.product_created))
                .SetSmallIcon(Resource.Mipmap.ic_launcher)
                .SetContentText(string.Format(context.GetString(Resource.String.product_created_template), model.Name, model.Price, model.Count))
                .SetContentIntent(pendingIntent)
                .SetPriority(NotificationCompat.PriorityDefault)
                .SetAutoCancel(true);

            NotificationManagerCompat nm = NotificationManagerCompat.From(context);
            nm.Notify(_notificationId++, notificationBuilder.Build());
        }

        private void CreateNotificationChannel(Context context)
        {
            NotificationChannel notificationChannel = new NotificationChannel(_channelId, _channelName, NotificationManager.ImportanceLow);
            notificationChannel.EnableLights(true);
            notificationChannel.LightColor = Color.Red;
            notificationChannel.EnableVibration(true);
            notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });

            NotificationManager notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            notificationManager.CreateNotificationChannel(notificationChannel);


        }
    }
}