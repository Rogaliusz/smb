using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using projekt_1.Repositories;
using projekt_1.Services.Geolocation;

namespace projekt_1.Activities.Users
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class LoginActivity : ActivityBase
    {
        private EditText _txtEmail;
        private EditText _txtPassword;
        private Button _btnLogin;
        private Button _btnRegister;

        private IUserRepository _userRepository;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_activity);

            Xamfire.Android.Xamfire.Initialization(this);
            IoC.MainContainer.RegisterIoC(this);

            _txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            _txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            _btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            _btnRegister = FindViewById<Button>(Resource.Id.btnGoToRegister);

            _userRepository = GetInstance<IUserRepository>();

            _btnLogin.Click += LoginHandler();
            _btnRegister.Click += GoToRegisterHandler();
        }

        private EventHandler LoginHandler()
            => async(e, s) => 
            {
                try
                {
                    await _userRepository.LoginAsync(_txtEmail.Text, _txtPassword.Text);
                    StartActivity(typeof(MainActivity));
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, Resource.String.login_problem, ToastLength.Long);
                }
            };

        private EventHandler GoToRegisterHandler()
            => (e, s) => { StartActivity(typeof(RegisterActivity)); };
    }
}