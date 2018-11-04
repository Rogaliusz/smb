using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using projekt_1.Models;
using projekt_1.Settings;
using SQLite;

namespace projekt_1.Services.SqlLite
{
    public class SqlLiteService : ISqlLiteService
    {
        public SQLiteConnection SqlConnection  {get; private set;}

        private readonly SqlLiteSettings _settings;

        public SqlLiteService(SqlLiteSettings settings)
        {
            _settings = settings;
        }

        public void CreateDb()
        {
            var db = new SQLiteConnection(SqlLiteSettings.Path);
            db.CreateTable<Product>();

            SqlConnection = db;
        }
    }
}