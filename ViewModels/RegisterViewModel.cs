using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Firebase.Auth;
using Microsoft.Extensions.Configuration;
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
using UniversalYoga.Views.IndicatorView;
using IToast = UniversalYoga.Services.Interface.IToast;

namespace UniversalYoga.ViewModels
{
    public class RegisterViewModel : BaseViewModel
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
        private string _ConfirmPassword;

        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set { _ConfirmPassword = value; OnPropertyChanged(); }
        }

        public clsUser User { get; set; }
        public FirebaseWebApi webApi;

        private readonly IUser _userService;
        private readonly IToast _toast;
        public ICommand LoginCMD { get; set; }
        public ICommand SignupCMD { get; set; }
        #endregion
        public RegisterViewModel()
        {
            EmailColor = Color.FromHex("#FF0000");
            User = new clsUser();
            webApi = new FirebaseWebApi();
            _userService = DependencyService.Resolve<IUser>();
            _toast = DependencyService.Resolve<IToast>();
            LoginCMD = new Command(Login);
            SignupCMD = new Command(SignupAsync);
        }
        #region Functions
        public async void SignupAsync()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                try
                {
                    if (String.IsNullOrEmpty(User.FirstName))
                    {
                        await _toast.Show("Enter your First Name.");
                    }
                    else if (String.IsNullOrEmpty(User.LastName))
                    {
                        await _toast.Show("Enter your Last Name.");
                    }
                    else if (String.IsNullOrEmpty(User.Email))
                    {
                        await _toast.Show("Enter your Email.");
                    }
                    else if (String.IsNullOrEmpty(User.Contact))
                    {
                        await _toast.Show("Enter your Contact.");
                    }
                    else if (String.IsNullOrEmpty(User.password))
                    {
                        await _toast.Show("Enter your Password.");
                    }
                    else if (String.IsNullOrEmpty(ConfirmPassword))
                    {
                        await _toast.Show("Confirm Password.");
                    }
                    else if (User.password.Length < 5)
                    {
                        await _toast.Show("Please enter 6 characters in your password.");
                    }
                    else if (EmailColor == Color.FromHex("#FF0000"))
                    {
                        await _toast.Show("Please enter valid email.");
                    }
                    else
                    {
                        if (User.password == ConfirmPassword)
                        {
                            IsBusy = true;
                            User.Email = User.Email.ToLower().Trim();
                            /*FirebaseAuthProvider authenticates the users.
                                FirebaseAuthentication.net Plugin must be installed.*/
                            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApi.WebAPIKey));
                            var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(User.Email.Trim().ToLower(), User.password);
                            /*If user is created through auth. User data will be save in Firebase DB.*/
                            await _userService.RegisterUser(User);
                            Preferences.Set("Address", User.Address.Trim().ToLower());
                            Preferences.Set("Contact", User.Contact.Trim().ToLower());
                            Preferences.Set("Email", User.Email.Trim().ToLower());
                            Preferences.Set("Name", User.FirstName.Trim()+" "+User.LastName.Trim());
                            IsBusy = false;
                            await Application.Current.MainPage.DisplayAlert("", "You are Successfully Registered.", "Ok");
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        else
                        {
                            await _toast.Show("Enter the Same Password.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert("", "Account already exist", "Ok");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", "Connect your device to internet.", "OK");
            }
        }

        public async void Login()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion
    }
}
