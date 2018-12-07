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
using projekt_1.Repositories;

namespace projekt_1.Activities.Users
{
    [Activity]
    public class RegisterActivity : ActivityBase
    {
        private EditText _txtEmail;
        private EditText _txtPassword;
        private Button _btnRegister;
        private Button _btnGoToLogin;

        private IUserRepository _userRepository;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.register_activity);

            _txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            _txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            _btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            _btnGoToLogin = FindViewById<Button>(Resource.Id.btnGoToLogin);

            _userRepository = GetInstance<IUserRepository>();

            _btnRegister.Click += RegisterAsync();
            _btnGoToLogin.Click += GoToLogin();
        }

        private EventHandler RegisterAsync()
            => async (e, s) => 
            {
                try
                {
                    await _userRepository.RegisterAsync(_txtEmail.Text, _txtPassword.Text);
                    Toast.MakeText(this, Resource.String.user_registered, ToastLength.Long).Show();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, Resource.String.register_problem, ToastLength.Long).Show();
                    GoToLogin().Invoke(e, s);
                }
            };

        private EventHandler GoToLogin()
            => (e, s) => { StartActivity(typeof(LoginActivity)); };
    }
}