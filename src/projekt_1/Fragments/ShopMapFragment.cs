using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.OS;
using Android.Views;
using projekt_1.Models;
using projekt_1.Repositories.Settings;
using projekt_1.Services.Geolocation;

namespace projekt_1.Fragments
{
    public class ShopMapFragment : FragmentBase, IOnMapReadyCallback, IFragment
    {
        private readonly IGeolocationService _geolocationService;
        private readonly ISettingsRepository _settingsRepository;

        private CircleOptions _pointer;
        private GoogleMap _googleMap;
        private System.Timers.Timer _timer;

        public ShopMapFragment(IGeolocationService geolocationService, ISettingsRepository settingsRepository)
        {
            _geolocationService = geolocationService;
            _settingsRepository = settingsRepository;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.map_fragment, container, false);
            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            var mapFragment = (SupportMapFragment)ChildFragmentManager
                .FindFragmentById(Resource.Id.map);

            if(mapFragment == null)
            {
                return;
            }

            mapFragment.GetMapAsync(this);
        }

        public async void OnMapReady(GoogleMap googleMap)
        {
            _googleMap = googleMap;
            _timer = new System.Timers.Timer(10 * 1000);
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();

            await SetCurrentPostitionAsync();

            var user = _settingsRepository.User;

            _googleMap.MyLocationEnabled = true;

            foreach (var shop in user.Shops)
            {
                googleMap.AddMarker(CreateMarkerOptions(shop));
                googleMap.AddCircle(CreateCircleOptions(shop));
            }
        }

        private async void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var currentLocation = await _geolocationService.GetCurrentGeolocationAsync();
            var location = new LatLng(currentLocation.Latitude, currentLocation.Longitude);
        }

        private async Task SetCurrentPostitionAsync()
        {
            var currentLocation = await _geolocationService.GetCurrentGeolocationAsync();
            var location = new LatLng(currentLocation.Latitude, currentLocation.Longitude);

            var builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(18);
            builder.Bearing(155);
            builder.Tilt(65);

            var cameraPosition = builder.Build();
            var cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);

            _googleMap.MoveCamera(cameraUpdate);
        }

        private CircleOptions CreateCircleOptions(Shop shop)
        {
            return new CircleOptions()
                .InvokeCenter(new LatLng(shop.Latitude, shop.Longitude))
                .InvokeRadius(shop.Radius)
                .InvokeFillColor(Color.Red);
        }

        private MarkerOptions CreateMarkerOptions (Shop shop)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(shop.Latitude, shop.Longitude));
            marker.SetTitle(shop.Name);
            marker.SetSnippet(shop.Description);

            return marker;
        } 
    }
}
