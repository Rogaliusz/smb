using Android.Support.V4.App;
using projekt_1.Fragments;

namespace projekt_1.Adapters.Pager
{
    public class MainActivityPagerAdapter : PageAdapterBase
    {
        public override int Count { get; }

        public MainActivityPagerAdapter(FragmentManager fm, int tabCount) : base(fm)
        {
            Count = tabCount;
        }

        public override Fragment GetItem(int position)
        {
            switch(position)
            {
                case 0:
                    return ResolveFragment<ProductListFragment>();
                case 1:
                    return ResolveFragment<ShopListFragment>();
                case 2:
                    return ResolveFragment<ShopMapFragment>();
                case 3:
                    return ResolveFragment<SettingsFragment>();
            }

            return null;
        }
    }
}
