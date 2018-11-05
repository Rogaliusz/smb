using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using projekt_2.AndroidServices;

[assembly:UsesPermission(Name = common.Permissions.TriggerOnProductCreatedPermission.Name)]
namespace projekt_2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        private Button _btnStart;
        private Button _btnStop;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            _btnStart = FindViewById<Button>(Resource.Id.btnStart);
            _btnStop = FindViewById<Button>(Resource.Id.btnStop);

            _btnStart.Click += (s, e) => { StartService(new Android.Content.Intent(this, typeof(ProductNotificationAndroidService))); };
            _btnStop.Click += (s, e) => { StopService(new Android.Content.Intent(this, typeof(ProductNotificationAndroidService))); };
        }
    }
}