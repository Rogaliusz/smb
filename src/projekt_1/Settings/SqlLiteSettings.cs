﻿using System;
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

namespace projekt_1.Settings
{
    public class SqlLiteSettings
    {
        public static string Path => System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "products.db3");
    }
}