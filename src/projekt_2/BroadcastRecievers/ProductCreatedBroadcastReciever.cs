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
using common.Models;
using common.Permissions;
using common.Services.Json;

namespace projekt_2.BroadcastRecievers
{
    public class ProductCreatedBroadcastReciever : BaseBroadcastReciever
    {
        private readonly IJsonService _jsonService;

        public ProductCreatedBroadcastReciever()
        {
            _jsonService = IoC.Container.GetInstance<IJsonService>();
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var serializedModel = intent.GetStringExtra(common.Extras.PRODUCT);
            var model = _jsonService.Deserialize<Product>(serializedModel);

        }
    }
}