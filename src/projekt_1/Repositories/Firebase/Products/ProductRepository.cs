using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using projekt_1.Models;
using projekt_1.Repositories.Firebase.Contexts;
using projekt_1.Repositories.Products;
using projekt_1.Repositories.Settings;

namespace projekt_1.Repositories.Firebase.Products
{
    public class ProductRepository : IFirebaseRepository, IProductRepository
    {
        private readonly UsersContext _usersContext;
        private readonly ISettingsRepository _settingsRepository;
        private readonly User _user;

        public ProductRepository(UsersContext usersContext, ISettingsRepository settingsRepository)
        {
            _usersContext = usersContext;
            _settingsRepository = settingsRepository;
            _user = _settingsRepository.User;
        }

        public void Delete(int id)
        {
            var product = _user.Products.FirstOrDefault(x => x.Id == id);
            _user.Products.Remove(product);

            _usersContext.InsertOrUpdateAsync(_user);
        }

        public Product GetProduct(int id)
        {
            return _user.Products.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _user.Products;
        }

        public void Insert(Product product)
        {
            if (_user.Products.Count == 0)
            {
                product.Id = 1;
            }
            else
            {
                product.Id = _user.Products.Count + 1;
            }

            _user.Products.Add(product);
            _usersContext.InsertOrUpdateAsync(_user);
        }

        public void Update(Product product)
        {
            var productOld = _user.Products.FirstOrDefault(x => x.Id == product.Id);
            _user.Products.Remove(productOld);
            _user.Products.Add(product);

            _usersContext.InsertOrUpdateAsync(_user);
        }
    }
}