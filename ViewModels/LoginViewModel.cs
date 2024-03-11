using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Firebase.Auth;
using Mopups.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniversalYoga.Helpers;
using UniversalYoga.Models;
using UniversalYoga.Services.Interface;
using UniversalYoga.Views;
using UniversalYoga.Views.IndicatorView;
using IToast = UniversalYoga.Services.Interface.IToast;

namespace UniversalYoga.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region
        private bool _IsBusy;
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                _IsBusy = value;
                if (_IsBusy)
                {
                    MopupService.Instance.PushAsync(new LoadingView());

                }
                else
                {
                    MopupService.Instance.PopAllAsync();
                }

                OnPropertyChanged();
            }
        }
        private Color _EmailColor;

        public Color EmailColor
        {
            get { return _EmailColor; }
            set { _EmailColor = value; OnPropertyChanged(); }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; OnPropertyChanged(); }
        }
        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(); }
        }
        private bool _remember;
        public bool remember
        {
            get { return _remember; }
            set { _remember = value; OnPropertyChanged(); }
        }

        public FirebaseWebApi webApi;

        private readonly IUser _userService;
        private readonly IToast _toast;
        public ICommand LoginCMD { get; set; }
        public ICommand SignupCMD { get; set; }
        #endregion
        public LoginViewModel()
        {
            EmailColor = Color.FromHex("#FF0000");
            Email = string.Empty;
            Password = string.Empty;
            webApi = new FirebaseWebApi();
            _userService = DependencyService.Resolve<IUser>();
            _toast = DependencyService.Resolve<IToast>();
            LoginCMD = new Command(Login);
            SignupCMD = new Command(Signup);
        }
        public async void Login()
        {

            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                if (String.IsNullOrEmpty(Email))
                {
                    await _toast.Show("Please enter your email.");
                }
                else if (String.IsNullOrEmpty(Password))
                {
                    await _toast.Show("Please enter your password.");
                }
                else if (Password.Length<5)
                {
                    await _toast.Show("Please enter 6 characters in your password.");
                }
                else
                {
                    if (Email != string.Empty || Password != string.Empty)
                    {
                        var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApi.WebAPIKey));
                        try
                        {
                            IsBusy = true;
                            var response = await _userService.LoginUser(Email.Trim().ToLower());
                            if (response != null)
                            {
                                var auth = await authProvider.SignInWithEmailAndPasswordAsync(Email.Trim().ToLower(), Password);
                                var content = await auth.GetFreshAuthAsync();
                                var serializedcontnet = JsonConvert.SerializeObject(content);
                                if (remember == true)
                                {
                                    Preferences.Set("Login", "User");
                                }
                                Preferences.Set("Email", response.Email.Trim().ToLower());
                                Preferences.Set("Address", response.Address.Trim().ToLower());
                                Preferences.Set("Contact", response.Contact.Trim().ToLower());
                                Preferences.Set("Name", response.FirstName.Trim() + " " + response.LastName.Trim());
                                IsBusy = false;
                                Application.Current.MainPage = new AppShell();
                            }
                            else
                            {
                                IsBusy = false;
                                await Application.Current.MainPage.DisplayAlert("", "Invalid email or password", "OK");
                            }
                        }
                        catch (Exception ex)
                        {
                            IsBusy = false;
                            await Application.Current.MainPage.DisplayAlert("", "Invalid email or password", "OK");
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("", "Email and Password are necessary.", "OK");
                    }
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", "Connect your device to internet.", "OK");
            }
        }

        public async void Signup()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
    }
}
