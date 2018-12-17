
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

namespace projekt_1.Activities.Shops
{
    [Activity(Label = "EditShopActivity", Theme = "@style/Theme.AppCompat.Light")]
    public class EditShopActivity : ShopActivityBase
    {
        protected override async void DoneClick()
        {
            var model = GetModel();
            await _shopRepository.UpdateAsync(model);

            OnBackPressed();
        }

        protected override string GetButtonName()
            => "Update";

        protected override string GetTitle()
            => "Update shop";

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var extras = Intent.Extras;
            var stringID = Intent.GetStringExtra(common.Extras.ID);

            var id = new Guid (Intent.GetStringExtra(common.Extras.ID));
            var model = await _shopRepository.GetAsync(id);

            _id = id;
            _txtDescrition.Text = model.Description;
            _txtName.Text = model.Name;
            _sbRadius.Progress = model.Radius;
        }
    }
}
