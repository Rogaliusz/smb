using System;
using System.Linq;
using System.Reflection;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase;
using projekt_1.Services.SqlLite;

namespace projekt_1.Activities
{
    [Activity]
    public class MainActivity : ActivityBase
    {
        private readonly ISqlLiteService _sqlLiteService = GetInstance<ISqlLiteService>();

        private Button _btnGoToSettings;
        private Button _btnGoToProducts;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            _btnGoToSettings = FindViewById<Button>(Resource.Id.btnGoToSettings);
            _btnGoToProducts = FindViewById<Button>(Resource.Id.btnGoToProducts);

            _btnGoToSettings.Click += (s, e) => { StartActivity(typeof(SettingsActivity)); };
            _btnGoToProducts.Click += (s, e) => { StartActivity(typeof(ProductListActivity)); };

            RegisterComponents();
        }

        private void RegisterComponents()
        {
            _sqlLiteService.CreateDb();
        }


    }
}

