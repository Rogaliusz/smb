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
using projekt_1.Services.SqlLite;

namespace projekt_1.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ISqlLiteService _sqlLiteService;

        public ProductRepository(ISqlLiteService sqlLiteService)
        {
            _sqlLiteService = sqlLiteService;
        }

        public void Insert(Product product)
        {
            _sqlLiteService.SqlConnection.Insert(product);
            _sqlLiteService.SqlConnection.Commit();
        }

        public void Update(Product product)
        {
            _sqlLiteService.SqlConnection.Update(product);
            _sqlLiteService.SqlConnection.Commit();
        }

        public void Delete(int id)
        {
            _sqlLiteService.SqlConnection.Delete<Product>(id);
            _sqlLiteService.SqlConnection.Commit();
        }

        public Product GetProduct(int id)
            => _sqlLiteService.SqlConnection.Find<Product>(id);

        public IEnumerable<Product> GetProducts()
            => _sqlLiteService.SqlConnection.Table<Product>();
    }
}