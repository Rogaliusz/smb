using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Autofac;
using common.Permissions;
using projekt_1.IoC;

[assembly: Permission(Name = TriggerOnProductCreatedPermission.Name, PermissionGroup = ProductsGroupPermission.Name, Label = TriggerOnProductCreatedPermission.Label)]
[assembly: UsesPermission(Name = TriggerOnProductCreatedPermission.Name)]

namespace projekt_1.Activities
{
    public abstract class ActivityBase : AppCompatActivity
    {
        protected static TInstance GetInstance<TInstance>()
            => MainContainer.Container.Resolve<TInstance>();

        protected static TInstance GetInstance<TInstance>(Context ctx)
            => MainContainer.Container.Resolve<TInstance>();


    }
}