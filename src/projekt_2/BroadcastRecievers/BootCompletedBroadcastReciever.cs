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
using projekt_2.AndroidServices;

namespace projekt_2.BroadcastRecievers
{
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    public class BootCompletedBroadcastReciever : BaseBroadcastReciever
    {
        public override void OnReceive(Context context, Intent intent)
        {
            context.StartService(new Intent(context, typeof(ProductNotificationAndroidService)));
        }
    }
}