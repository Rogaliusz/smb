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
using projekt_1.Models;
using projekt_1.Repositories.Products;

namespace projekt_1.Activities.Products
{
    public abstract class ProductAcitivityBase : ActivityBase
    {
        protected readonly IProductRepository _productRepository = GetInstance<IProductRepository>();

        protected TextView _txtName;
        protected TextView _txtCount;
        protected TextView _txtPrice;

        protected Button _btnDone;

        protected int _id = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.product_activity);

            _txtName = FindViewById<TextView>(Resource.Id.txtName);
            _txtCount = FindViewById<TextView>(Resource.Id.txtCount);
            _txtPrice = FindViewById<TextView>(Resource.Id.txtPrice);

            _btnDone = FindViewById<Button>(Resource.Id.btnDone);
            _btnDone.Click += (e, s) => { DoneClick(); };
            _btnDone.Text = GetButtonName();

            Title = GetTitle();
        }

        protected abstract string GetButtonName();

        protected abstract string GetTitle();

        protected abstract void DoneClick();

        protected Product GetModel()
            => new Product
            {
                Id = _id,
                Count = Int32.Parse(_txtCount.Text),
                Price = float.Parse(_txtPrice.Text),
                Name = _txtName.Text
            };
        
    }
}