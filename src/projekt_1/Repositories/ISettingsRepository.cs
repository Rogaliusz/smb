using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using projekt_1.Models;

namespace projekt_1.Repositories.Settings
{
    public interface ISettingsRepository : IRepository
    {
        int Size { get; set; }

        Color Color { get; set; }

        User User { get; set; }
    }
}