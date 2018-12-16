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
using projekt_1.Extensions;

namespace projekt_1.Models
{
    public class User
    {
        public string Email { get; set; }

        public string UserId { get => Email.Base64Encode(); set => Email = value.Base64Decode(); }

        public IList<Product> Products { get; set; } = new List<Product>();

        public IList<Shop> Shops { get; set; } = new List<Shop>();

        public User(string email)
        {
            Email = email;
        }
    }
}