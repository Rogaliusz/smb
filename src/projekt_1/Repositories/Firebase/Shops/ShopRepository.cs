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
using projekt_1.Models;
using projekt_1.Repositories.Firebase.Contexts;
using projekt_1.Repositories.Settings;

namespace projekt_1.Repositories.Firebase.Shops
{
    public class ShopRepository : IShopRepository, IFirebaseRepository
    {
        private readonly UsersContext _usersContext;
        private readonly ISettingsRepository _settingsRepository;
        private readonly User _user;

        public ShopRepository(UsersContext usersContext, ISettingsRepository settingsRepository)
        {
            _usersContext = usersContext;
            _settingsRepository = settingsRepository;
            _user = _settingsRepository.User;
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