using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Plugin.CurrentActivity;
using projekt_1.Adapters.Pager;
using projekt_1.BroadcastRecievers;
using projekt_1.Services.Geolocation;
using projekt_1.Services.SqlLite;

namespace projekt_1.Activities
{
    [Activity(Theme = "@style/Theme.AppCompat.Light")]
    public class MainActivity : ActivityBase
    {
        private readonly ISqlLiteService _sqlLiteService = GetInstance<ISqlLiteService>();
        private readonly IGeolocationService _geolocationService = GetInstance<IGeolocationService>();

        private TabLayout _tabLayout;
        private ViewPager _viewPager;
        private BroadcastReceiver _broadcastReciever;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            _tabLayout = FindViewById<TabLayout>(Resource.Id.tab_layout);
            _viewPager = FindViewById<ViewPager>(Resource.Id.pager);

            InitalizeTabLayout();
            InitalizeViewPager();

            RegisterComponents();

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            _geolocationService.StartListeningGeolocationAsync();

            CreateBroadcastReciever();
        }

        private void InitalizeTabLayout()
        {
            _tabLayout.AddTab(CreateTab("products"));
            _tabLayout.AddTab(CreateTab("shops"));
            _tabLayout.AddTab(CreateTab("map"));
            _tabLayout.AddTab(CreateTab("settings"));

            _tabLayout.TabGravity = TabLayout.GravityCenter;

            _tabLayout.TabSelected += _tabLayout_Click;
        }

        private void _tabLayout_Click(object sender, TabLayout.TabSelectedEventArgs e)
        {
            _viewPager.CurrentItem = e.Tab.Position;
        }

        private TabLayout.Tab CreateTab(string name)
            => _tabLayout.NewTab().SetText(name);

        private void InitalizeViewPager()
        {
            var adapter = new MainActivityPagerAdapter(SupportFragmentManager, _tabLayout.TabCount);

            _viewPager.Adapter = adapter;
        }

        private void RegisterComponents()
        {
            _sqlLiteService.CreateDb();
            CreateBroadcastReciever();
        }

        private void CreateBroadcastReciever()
        {
            if(_broadcastReciever != null)
            {
                return;
            }

            _broadcastReciever = new GeofenceBroadcastReciever();
            var intentFilter = new IntentFilter(Constans.GeofenceIntent);
            RegisterReceiver(_broadcastReciever, intentFilter);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

