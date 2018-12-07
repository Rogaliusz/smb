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
using projekt_1.Repositories.Settings;

namespace projekt_1.Repositories.Memory
{
    public class InMemorySettings : ISettingsRepository
    {
        public int Size { get; set; }
        public Color Color { get; set; }
        public User User { get; set; }
    }
}