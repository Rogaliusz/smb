using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using projekt_1.Activities.Products;
using projekt_1.Adapters;

namespace projekt_1.Activities
{
    [Activity(Label = "Products")]
    public class ProductListActivity : ActivityBase
    {
        private FloatingActionButton _fab;
        private ListView _lst;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.products_activity);

            _lst = FindViewById<ListView>(Resource.Id.lstProducts);
            _fab = FindViewById<FloatingActionButton>(Resource.Id.fabAdd);

            _lst.Adapter = GetInstance<ProductListAdapter>(this);
            _fab.Click += (e, s) => { StartActivity(typeof(AddProductActivity)); };
        }
    }
}