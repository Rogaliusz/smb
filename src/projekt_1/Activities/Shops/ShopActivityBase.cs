using System;
using Android.App;
using Android.OS;
using Android.Widget;
using projekt_1.Models;
using projekt_1.Repositories;
using projekt_1.Services.Geolocation;

namespace projekt_1.Activities.Shops
{
    [Activity(Theme = "@style/Theme.AppCompat.Light")]
    public abstract class ShopActivityBase : ActivityBase
    {
        protected readonly IShopRepository _shopRepository = GetInstance<IShopRepository>();
        protected readonly IGeolocationService _geolocationService = GetInstance<IGeolocationService>();

        protected TextView _txtName;
        protected TextView _txtDescrition;
        private TextView _txtRadiusValue;

        protected SeekBar _sbRadius;

        protected Button _btnDone;

        protected Guid _id = Guid.NewGuid();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.shop_activity);

            _txtName = FindViewById<TextView>(Resource.Id.txtName);
            _txtDescrition = FindViewById<TextView>(Resource.Id.txtDescription);
            _txtRadiusValue = FindViewById<TextView>(Resource.Id.txtRadiusValue);
            _sbRadius = FindViewById<SeekBar>(Resource.Id.sbRadius);

            _btnDone = FindViewById<Button>(Resource.Id.btnDone);
            _btnDone.Click += (e, s) => { DoneClick(); };
            _btnDone.Text = GetButtonName();

            _sbRadius.ProgressChanged += sbRadius_ProgressChanged;

            Title = GetTitle();
        }

        private void sbRadius_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            SetRadiusValue(e.Progress);
        }

        protected void SetRadiusValue(int progress)
        {
            _txtRadiusValue.Text = $"Radius: {progress}";
        }

        protected abstract string GetButtonName();

        protected abstract string GetTitle();

        protected abstract void DoneClick();

        protected Shop GetModel()
            => new Shop
            {
                Id = _id,
                Description = _txtDescrition.Text,
                Radius = _sbRadius.Progress,
                Name = _txtName.Text
            };

    }
}
