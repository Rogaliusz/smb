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
using SQLite;

namespace projekt_1.Services.SqlLite
{
    public interface ISqlLiteService : IService
    {
        void CreateDb();

        SQLiteConnection SqlConnection { get; }
    }
}