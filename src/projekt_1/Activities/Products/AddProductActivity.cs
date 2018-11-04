﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace projekt_1.Activities.Products
{
    [Activity(Label="Add new product")]
    public class AddProductActivity : ProductAcitivityBase
    {
        protected override void DoneClick()
        {
            _productRepository.Insert(GetModel());
            StartActivity(typeof(ProductListActivity));
        }

        protected override string GetButtonName()
            => "Insert";

        protected override string GetTitle()
            => "Insert new product";
    }
}