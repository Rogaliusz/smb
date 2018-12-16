using System;
using Android.Content;
using Android.Support.V4.App;
using Autofac;
using projekt_1.IoC;

namespace projekt_1.Fragments
{
    public class FragmentBase : Fragment
    {
        protected static TInstance GetInstance<TInstance>()
            => MainContainer.Container.Resolve<TInstance>();

        protected static TInstance GetInstance<TInstance>(Context ctx)
            => MainContainer.Container.Resolve<TInstance>();

    }
}
