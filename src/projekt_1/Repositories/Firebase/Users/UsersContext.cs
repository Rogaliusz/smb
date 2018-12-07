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
using Xamfire.Contexts;
using Xamfire.Contexts.Configurations;

namespace projekt_1.Repositories.Firebase.Contexts
{
    public class UsersContext : FirebaseContextBase<User>
    {
        public override void Configure(IModelConfiguration<User> modelConfiguration)
        {
            modelConfiguration.SetDocumentPath("users")
                .SetPrimaryKey(user => user.UserId);
        }
    }
}