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
using projekt_2.Services.Notification;

namespace projekt_2.BroadcastRecievers
{
    public class ProductCreatedBroadcastReciever : BaseBroadcastReciever
    {
        private static string _channelName = "channel1";
        private static string _channelId = "666";
        private static int _pendingIntentId = 0;

        private readonly IJsonService _jsonService;
        private readonly INotificationService _notificationService;

        public ProductCreatedBroadcastReciever(IJsonService jsonService, INotificationService notificationService)
        {
            _jsonService = jsonService;
            _notificationService = notificationService;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var serializedModel = intent.GetStringExtra(common.Extras.PRODUCT);
            var model = _jsonService.Deserialize<Product>(serializedModel);

            Toast.MakeText(context, $"Recieved Information for product {model.Id} {model.Name}", ToastLength.Long);

            var notificationClickedIntent = new Intent(common.Intents.GO_TO_PRODUCT_EDIT);
            notificationClickedIntent.PutExtra(common.Extras.ID, model.Id.ToString());

            var pendingIntent = PendingIntent.GetActivity(context, _pendingIntentId++, notificationClickedIntent, PendingIntentFlags.UpdateCurrent);

            _notificationService.CreateNotificationChannel(_channelId, _channelName);
            _notificationService.SendNotificationToChannel(_channelId,
                context.GetString(Resource.String.product_created),
                Resource.Mipmap.ic_launcher,
                string.Format(context.GetString(Resource.String.product_created_template), model.Name, model.Price, model.Count, model.Id),
                pendingIntent,
                NotificationCompat.PriorityDefault,
                true );
        }


    }
}