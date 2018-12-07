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
using projekt_1.Repositories.Firebase;

namespace projekt_1.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task RegisterAsync(string username, string password);
        Task LoginAsync(string username, string password);
    }
}