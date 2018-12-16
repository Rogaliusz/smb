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
using common.Services;
using projekt_1.Adapters;
using projekt_1.Adapters.List;
using projekt_1.Fragments;
using projekt_1.Repositories;
using projekt_1.Repositories.Firebase;
using projekt_1.Repositories.Firebase.Contexts;
using projekt_1.Repositories.Memory;
using projekt_1.Repositories.Settings;
using projekt_1.Services;
using projekt_1.Settings;
using Xamfire.Contexts.Auth;

namespace projekt_1.IoC
{
    public static class MainContainer
    {
        public  static IContainer Container { get; private set; }

        private static Assembly Assembly => typeof(MainContainer).Assembly;

        static MainContainer()
        {
        }

        public static void RegisterIoC(Context context)
        {
            var builder = new ContainerBuilder();
            RegisterModules(builder);

            builder.RegisterInstance(context)
                .SingleInstance();

            Container = builder.Build();
        }

        private static void RegisterModules(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly)
                .Where(x => x.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterAssemblyTypes(typeof(ICommonService).Assembly)
                .Where(x => x.IsAssignableTo<ICommonService>())
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterAssemblyTypes(Assembly)
                .Where(x => x.IsAssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterAssemblyTypes(Assembly)
                .Where(x => x.IsAssignableTo<FragmentBase>())
                .AsSelf()
                .SingleInstance();

            builder.RegisterAssemblyTypes(Assembly)
                .Where(x => x.IsAssignableTo<IFirebaseRepository>())
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<SqlLiteSettings>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<FirebaseSettings>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<ProductListAdapter>()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterType<ShopListAdapter>()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterInstance<IAuthenticationContext>(Xamfire.Xamfire.AuthenticationContext)
                .SingleInstance();

            builder.RegisterInstance<ISettingsRepository>(new InMemorySettings())
                .SingleInstance();

            builder.RegisterType<UsersContext>()
                .AsSelf()
                .SingleInstance();
        }


    }
}