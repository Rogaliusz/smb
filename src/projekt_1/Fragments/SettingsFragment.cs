using System;

using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using projekt_1.Repositories.Settings;

namespace projekt_1.Fragments
{
    public class SettingsFragment : FragmentBase, IFragment
    {
        private Spinner _spnColor;
        private EditText _txtSize;
        private Button _btnSave;

        private ISettingsRepository _settingsRepository;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.settings_activity, container, false);

            _spnColor = view.FindViewById<Spinner>(Resource.Id.spnColor);
            _txtSize = view.FindViewById<EditText>(Resource.Id.txtSize);
            _btnSave = view.FindViewById<Button>(Resource.Id.btnSave);

            _settingsRepository = GetInstance<ISettingsRepository>(Context);

            var colors = new[] { Color.Black, Color.DarkRed, Color.Blue };
            var adapter = new ArrayAdapter<Color>(Context, Resource.Layout.support_simple_spinner_dropdown_item, colors);
            adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            _spnColor.Adapter = adapter;

            _btnSave.Click += OnSave_Clicked;

            var savedColor = _settingsRepository.Color;
            var positionForCurrentColor = adapter.GetPosition(savedColor);
            _spnColor.SetSelection(positionForCurrentColor);

            var savedSize = _settingsRepository.Size;
            _txtSize.Text = savedSize.ToString();

            return view;
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