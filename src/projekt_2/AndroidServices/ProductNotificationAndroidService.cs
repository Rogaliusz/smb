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
using common.Permissions;
using projekt_2.BroadcastRecievers;

namespace projekt_2.AndroidServices
{
    [Service(Exported = true)]
    public class ProductNotificationAndroidService : BaseAndroidService
    {
        Handler _handler;
        BroadcastReceiver _productCreatedBroadcastReciever;

        public override void OnCreate()
        {
            base.OnCreate();

            _handler = new Handler();
            _productCreatedBroadcastReciever = new ProductCreatedBroadcastReciever();

            var intentFilter = new IntentFilter(common.Intents.PRODUCT_CREATED);

            RegisterReceiver(_productCreatedBroadcastReciever, intentFilter);
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            return StartCommandResult.Sticky;
        }

        public override bool StopService(Intent name)
        {
            UnregisterReceiver(_productCreatedBroadcastReciever);

            return base.StopService(name);
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}