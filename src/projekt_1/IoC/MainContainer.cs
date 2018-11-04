using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;
using projekt_1.Adapters;
using projekt_1.Repositories;
using projekt_1.Services;
using projekt_1.Settings;

namespace projekt_1.IoC
{
    public static class MainContainer
    {
        public  static IContainer Container { get; private set; }

        private static Assembly Assembly => typeof(MainContainer).Assembly;

        static MainContainer()
        {
            var builder = new ContainerBuilder();
            RegisterModules(builder);

            Container = builder.Build();
        }

        private static void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly)
                .Where(x => x.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterAssemblyTypes(Assembly)
                .Where(x => x.IsAssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<SqlLiteSettings>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<ProductListAdapter>()
                .AsSelf()
                .InstancePerDependency();
        }
    }
}