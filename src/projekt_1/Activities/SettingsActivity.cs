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
using projekt_1.Repositories.Settings;

namespace projekt_1.Activities
{
    [Activity(Label = "@string/go_to_settings")]
    public class SettingsActivity : ActivityBase
    {

        private Spinner _spnColor;
        private EditText _txtSize;
        private Button _btnSave;

        private ISettingsRepository _settingsRepository;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.settings_activity);

            _spnColor = FindViewById<Spinner>(Resource.Id.spnColor);
            _txtSize = FindViewById<EditText>(Resource.Id.txtSize);
            _btnSave = FindViewById<Button>(Resource.Id.btnSave);

            _settingsRepository = GetInstance<ISettingsRepository>(this);

            var colors = new[] { Color.Black, Color.DarkRed, Color.Blue };
            var adapter = new ArrayAdapter<Color>(this, Resource.Layout.support_simple_spinner_dropdown_item, colors);
            adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            _spnColor.Adapter = adapter;

            _btnSave.Click += OnSave_Clicked;

            var savedColor = _settingsRepository.Color;
            var positionForCurrentColor = adapter.GetPosition(savedColor);
            _spnColor.SetSelection(positionForCurrentColor);

            var savedSize = _settingsRepository.Size;
            _txtSize.Text = savedSize.ToString();
                
        }

        private void OnSave_Clicked(object sender, EventArgs e)
        {
            var currentColor = ((ArrayAdapter<Color>)_spnColor.Adapter).GetItem(_spnColor.SelectedItemPosition);
            var currentSize = Int32.Parse(_txtSize.Text);

            _settingsRepository.Size = currentSize;
            _settingsRepository.Color = currentColor;

        }
    }
}