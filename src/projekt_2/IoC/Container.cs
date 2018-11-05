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
using common.Services.Json;
using projekt_2.BroadcastRecievers;
using projekt_2.Services.Notification;
using TinyIoC;

namespace projekt_2.IoC
{
    public static class Container
    {

        private static TinyIoCContainer _container;

        static Container()
        {
            _container = TinyIoC.TinyIoCContainer.Current;

            _container.Register<IJsonService, JsonService>().AsSingleton();
            _container.Register<INotificationService, NotificationService>().AsMultiInstance();

            _container.Register<ProductCreatedBroadcastReciever>();
        }

        public static void RegisterContext(Context context)
            => _container.Register(context);

        public static T GetInstance<T>() where T : class
            => _container.Resolve<T>();

        public static T GetInstance<T>(Context context) where T : class
            => _container.Resolve<T>(new NamedParameterOverloads(
                new Dictionary<string, object>
                    {{"Context", context } }));
    }
}