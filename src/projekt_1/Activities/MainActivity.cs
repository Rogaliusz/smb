using System;
using System.Linq;
using System.Reflection;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Firebase;
using projekt_1.Services.SqlLite;

namespace projekt_1.Activities
{
    [Activity]
    public class MainActivity : ActivityBase
    {
        private readonly ISqlLiteService _sqlLiteService = GetInstance<ISqlLiteService>();

        private Toolbar _toolbar;
        private TabLayout _tabLayout;
        private ViewPager _viewPager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            _toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            _tabLayout = FindViewById<TabLayout>(Resource.Id.tab_layout);
            _viewPager = FindViewById<ViewPager>(Resource.Id.pager);

            //SetSupportActionBar(_toolbar);

            InitalizeTabLayout();
            InitalizeViewPager();

            RegisterComponents();
        }

        private void InitalizeTabLayout()
        {
            _tabLayout.AddTab(CreateTab("products"));
            _tabLayout.AddTab(CreateTab("shops"));
            _tabLayout.AddTab(CreateTab("map"));
            _tabLayout.AddTab(CreateTab("settings"));
        }

        private TabLayout.Tab CreateTab(string name)
            => _tabLayout.NewTab().SetText(name);

        private void InitalizeViewPager()
        {

        }

        private void RegisterComponents()
        {
            _sqlLiteService.CreateDb();
        }


    }
}

