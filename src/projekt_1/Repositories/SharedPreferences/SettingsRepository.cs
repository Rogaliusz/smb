using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using projekt_1.Models;

namespace projekt_1.Repositories.Settings
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly ISharedPreferences _sharedPreferences;
        private ISharedPreferencesEditor _sharedPreferencesEditor;

        public int Size
        {
            get => _sharedPreferences.GetInt(nameof(Size), 10);
            set
            {
                _sharedPreferencesEditor.PutInt(nameof(Size), value);
                _sharedPreferencesEditor.Commit();
                _sharedPreferencesEditor = _sharedPreferences.Edit();
            }
        }
    
        public Color Color
        {
            get => new Color(_sharedPreferences.GetInt("color", Color.Black.ToArgb()));
            set
            {
                _sharedPreferencesEditor.PutInt("color", value.ToArgb());
                _sharedPreferencesEditor.Apply();
                _sharedPreferencesEditor.Commit();
                _sharedPreferencesEditor = _sharedPreferences.Edit();
            }
        }

        public User User
        {
            get;
            set;
        }

        public SettingsRepository(Context context)
        {
            _sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(context);
            _sharedPreferencesEditor = _sharedPreferences.Edit();
        }
    }
}