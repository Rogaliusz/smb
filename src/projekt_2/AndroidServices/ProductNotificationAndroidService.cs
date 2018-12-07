using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using common.Permissions;
using projekt_2.BroadcastRecievers;
using projekt_2.Services.Notification;

[assembly: Permission(Name = TriggerOnProductCreatedPermission.Name, PermissionGroup = ProductsGroupPermission.Name, Label = TriggerOnProductCreatedPermission.Label)]
[assembly: UsesPermission(Name = TriggerOnProductCreatedPermission.Name)]

namespace projekt_2.AndroidServices
{
    [Service(Exported = true)]
    public class ProductNotificationAndroidService : BaseAndroidService
    {
        private const string SERVICE_CHANNEL_NAME = "service";
        private const string SERVICE_CHANNEL_ID = "555";

        BroadcastReceiver _productCreatedBroadcastReciever;
        INotificationService _notificationService;

        int _serviceId = 1;

        public override void OnCreate()
        {
            base.OnCreate();

            Toast.MakeText(this, "Product service was created", ToastLength.Long);

            _productCreatedBroadcastReciever = IoC.Container.GetInstance<ProductCreatedBroadcastReciever>();
            _notificationService = IoC.Container.GetInstance<INotificationService>();

            _notificationService.CreateNotificationChannel(SERVICE_CHANNEL_ID, SERVICE_CHANNEL_NAME);

            RegisterProductCreatedBroadcastReciever();
        }

        private void RegisterProductCreatedBroadcastReciever()
        {
            var intentFilter = new IntentFilter(common.Intents.PRODUCT_CREATED);
            RegisterReceiver(_productCreatedBroadcastReciever, intentFilter, TriggerOnProductCreatedPermission.Name, null);
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            StartForeground(_serviceId++, GetNotification());
            return StartCommandResult.Sticky;
        }

        private Notification GetNotification()
        {
            Notification notification;
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, SERVICE_CHANNEL_ID);
            builder.SetColor(Resources.GetColor(Resource.Color.material_deep_teal_500))
                    .SetAutoCancel(true);

            notification = builder.Build();
            notification.Flags = NotificationFlags.ForegroundService | NotificationFlags.AutoCancel;

            return notification;
        }


        public override bool StopService(Intent name)
        {
            UnregisterReceiver(_productCreatedBroadcastReciever);

            Toast.MakeText(this, "Product service was stopped", ToastLength.Long);

            return base.StopService(name);
        }

        public override void OnDestroy()
        {
            Toast.MakeText(this, "Product service was destroyed", ToastLength.Long);
            StopForeground(true);
            base.OnDestroy();
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}