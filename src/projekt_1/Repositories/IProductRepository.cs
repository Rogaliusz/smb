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
using projekt_1.Repositories.Sqllite;

namespace projekt_1.Repositories.Products
{
    public interface IProductRepository : IRepository
    {
        void Insert(Product product);
        void Update(Product product);
        void Delete(int id);

        Product GetProduct(int id);
        IEnumerable<Product> GetProducts();
    }
}