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

        }

        public static T GetInstance<T>() where T : class
            => _container.Resolve<T>();

        public static T GetInstance<T>(Context context) where T : class
            => _container.Resolve<T>(new NamedParameterOverloads(
                new Dictionary<string, object>
                    {{"Context", context } }));
    }
}