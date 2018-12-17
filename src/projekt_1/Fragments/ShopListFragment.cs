using System;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using projekt_1.Activities.Products;
using projekt_1.Activities.Shops;
using projekt_1.Adapters;

namespace projekt_1.Fragments
{
    public class ShopListFragment : FragmentBase, IFragment
    {
        private FloatingActionButton _fab;
        private ListView _lst;
        private ShopListAdapter _adapter;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.shops_fragment, container, false);

            _lst = view.FindViewById<ListView>(Resource.Id.lstShops);
            _fab = view.FindViewById<FloatingActionButton>(Resource.Id.fabAdd);

            _adapter = GetInstance<ShopListAdapter>(Context);
            _lst.Adapter = _adapter;
            _fab.Click += (e, s) => { GoToAddShopActivity(); };

            return view;
        }

        private void GoToAddShopActivity()
        {
            var intent = new Intent(Context, typeof(AddShopActivity));

            StartActivity(intent);
        }

        public override void OnResume()
        {
            base.OnResume();

            _adapter.RefreshData();
        }
    }
}
