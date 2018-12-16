using System;
using Android.Support.V4.App;
using Autofac;
using projekt_1.IoC;

namespace projekt_1.Adapters.Pager
{
    public abstract class PageAdapterBase : FragmentStatePagerAdapter
    {
        public PageAdapterBase(FragmentManager fm) : base(fm)
        {
        }

        protected Fragment ResolveFragment<TFragment>()
            where TFragment : Fragment
        {
            return MainContainer.Container.Resolve<TFragment>();
        }
    }
}
