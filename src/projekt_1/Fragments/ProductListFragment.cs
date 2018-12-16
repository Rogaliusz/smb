using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using projekt_1.Activities.Products;
using projekt_1.Adapters;


namespace projekt_1.Fragments
{
    public class ProductListFragment : FragmentBase, IFragment
    {
        private FloatingActionButton _fab;
        private ListView _lst;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.products_activity, container, false);

            _lst = view.FindViewById<ListView>(Resource.Id.lstProducts);
            _fab = view.FindViewById<FloatingActionButton>(Resource.Id.fabAdd);

            _lst.Adapter = GetInstance<ProductListAdapter>(Context);
            _fab.Click += (e, s) => { GoToAddProductActivity(); };

            return view;
        }

        private void GoToAddProductActivity()
        {
            var intent = new Intent(Context, typeof(AddProductActivity));

            StartActivity(intent);
        }

        public override void OnAttach(Context context)
        {
            base.OnAttach(context);
        }
    }
}