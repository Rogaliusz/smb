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
using Autofac;
using projekt_1.IoC;

namespace projekt_1.Activities
{
    public abstract class ActivityBase : Activity
    {
        protected static TInstance GetInstance<TInstance>()
            => MainContainer.Container.Resolve<TInstance>();

        protected static TInstance GetInstance<TInstance>(Context ctx)
            => MainContainer.Container.Resolve<TInstance>(new NamedParameter("context", ctx));


    }
}