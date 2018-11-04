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
using common.Services.Json;
using projekt_1.Models;

namespace projekt_1.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly Context _context;
        private readonly IJsonService _jsonService;

        public ProductService(Context context, IJsonService jsonService)
        {
            _context = context;
            _jsonService = jsonService;
        }

        public void ProductWasCreated(Product product)
        {
            var intent = new Intent(common.Intents.PRODUCT_CREATED);
            var serializedProduct = _jsonService.Serialize<Product>(product);
            intent.PutExtra(common.Extras.PRODUCT, serializedProduct);

            _context.SendBroadcast(intent);
        }
    }
}