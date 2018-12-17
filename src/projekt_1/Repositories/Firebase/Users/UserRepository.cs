using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using projekt_1.Extensions;
using projekt_1.Repositories.Firebase.Contexts;
using projekt_1.Repositories.Settings;
using projekt_1.Services.Geolocation;
using projekt_1.Settings;
using Xamfire.Contexts.Auth;

namespace projekt_1.Repositories.Firebase.Users
{
    public class UserRepository : IUserRepository, IFirebaseRepository
    {
        private readonly IAuthenticationContext _authenticationContext;
        private readonly UsersContext _usersContext;
        private readonly ISettingsRepository _settingsRepository;
        private readonly IGeofenceService _geofenceService;

        public UserRepository(IAuthenticationContext authenticationContext, 
            UsersContext usersContext, 
            ISettingsRepository settingsRepository, 
            IGeofenceService geofenceService)
        {
            _authenticationContext = authenticationContext;
            _usersContext = usersContext;
            _settingsRepository = settingsRepository;
            _geofenceService = geofenceService;
        }

        public async Task LoginAsync(string username, string password)
        {
            await _authenticationContext.LoginUserAsync(username, password);
            var user = await _usersContext.GetAsync(username.Base64Encode());

            //TODO check this null 
            user.Shops = user.Shops.Where(x => x != null).ToList();
            user.Products = user.Products.Where(x => x != null).ToList();

            _settingsRepository.User = user;

            foreach (var shop in user.Shops)
            {
                _geofenceService.SetGeofenceTrigger(shop.Latitude, shop.Longitude, shop.Radius,
                    $"Entered into {shop.Name}", $"You leaved {shop.Name} Why ;-(!", shop.Name);
            }
        }

        public async Task RegisterAsync(string username, string password)
        {
            await _authenticationContext.RegisterUserAsync(username, password);
            await _authenticationContext.LoginUserAsync(username, password);
            await _usersContext.InsertOrUpdateAsync(new Models.User(username));
        }
    }
}