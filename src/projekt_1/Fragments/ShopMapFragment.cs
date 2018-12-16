using System;
using System.Threading.Tasks;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
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
            await SetCurrentPostitionAsync(googleMap);

            var user = _settingsRepository.User;

            foreach (var shop in user.Shops)
            {
                googleMap.AddMarker(CreateMarkerOptions(shop));
            }
        }

        private async Task SetCurrentPostitionAsync(GoogleMap googleMap)
        {
            var currentLocation = await _geolocationService.GetCurrentGeolocationAsync();

            LatLng location = new LatLng(currentLocation.Latitude, currentLocation.Longitude);

            CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
            builder.Target(location);
            builder.Zoom(18);
            builder.Bearing(155);
            builder.Tilt(65);

            var cameraPosition = builder.Build();
            var cameraUpdate = CameraUpdateFactory.NewCameraPosition(cameraPosition);
            googleMap.MoveCamera(cameraUpdate);
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
