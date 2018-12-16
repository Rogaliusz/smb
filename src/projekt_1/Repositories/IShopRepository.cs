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

namespace projekt_1.Repositories
{
    interface IShopRepository : IRepository
    {
        Task InsertAsync(Shop shop);
        Task UpdateAsync(Shop shop);
        Task DeleteAsync(Guid id);

        Task<Shop> GetAsync(Guid id);
        Task<IEnumerable<Shop>> GetAllAsync();
    }
}