using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projekt_1.Messanger;
using projekt_1.Messanger.Messages;
using projekt_1.Models;
using projekt_1.Repositories.Firebase.Contexts;
using projekt_1.Repositories.Settings;
using projekt_1.Services.Geolocation;

namespace projekt_1.Repositories.Firebase.Shops
{
    public class ShopRepository : IShopRepository, IFirebaseRepository
    {
        private readonly IMessanger _messanger;
        private readonly UsersContext _usersContext;
        private readonly ISettingsRepository _settingsRepository;
        private readonly User _user;
        private readonly IGeofenceService _geofenceService;

        public ShopRepository(IMessanger messenger,
            UsersContext usersContext, 
            ISettingsRepository settingsRepository,
            IGeofenceService geofenceService)
        {
            _messanger = messenger;
            _usersContext = usersContext;
            _settingsRepository = settingsRepository;
            _user = _settingsRepository.User;
            _geofenceService = geofenceService;
        }

        public async Task DeleteAsync(Guid id)
        {
            var shop = _user.Shops.FirstOrDefault(x => x.Id == id);
            _user.Shops.Remove(shop);

            await _usersContext.InsertOrUpdateAsync(_user);
        }

        public async Task<Shop> GetAsync(Guid id)
        {
            return _user.Shops.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<Shop>> GetAllAsync()
        {
            return _user.Shops;
        }

        public async Task InsertAsync(Shop shop)
        {
            _user.Shops.Add(shop);
            await _usersContext.InsertOrUpdateAsync(_user);

            _messanger.Publish(new ShopCreatedMessage(shop, this));
            _geofenceService.SetGeofenceTrigger(shop.Latitude, shop.Longitude, shop.Radius,
                $"Entered into {shop.Name}", $"You leaved {shop.Name} Why ;-(!", shop.Name);
        }

        public async Task UpdateAsync(Shop shop)
        {
            var oldShop = _user.Shops.FirstOrDefault(x => x.Id == shop.Id);
            _user.Shops.Remove(oldShop);
            _user.Shops.Add(shop);

            await _usersContext.InsertOrUpdateAsync(_user);
        }
    }
}