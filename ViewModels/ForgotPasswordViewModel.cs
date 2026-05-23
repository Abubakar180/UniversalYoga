using Firebase.Auth;
using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Controls;
using Mopups.PreBaked.Interfaces;
using Mopups.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniversalYoga.Helpers;
using UniversalYoga.Services.Interface;
using UniversalYoga.Views.IndicatorView;

namespace UniversalYoga.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel
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

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; OnPropertyChanged(); }
        }

        public FirebaseWebApi webApi;

        private readonly IUser _userService;
        private readonly IToast _toast;
        public ICommand SendCMD { get; set; }
        #endregion
        public ForgotPasswordViewModel()
        {
            Email = string.Empty;
            webApi = new FirebaseWebApi();
            _userService = DependencyService.Resolve<IUser>();
            _toast = DependencyService.Resolve<IToast>();
            SendCMD = new Command(SendPassword2Email);
        }
        public async void SendPassword2Email()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                if (String.IsNullOrEmpty(Email))
                {
                    await _toast.Show("Please enter your email.");
                }
                else
                {
                    /*FirebaseAuthProvider authenticates the users.
                     FirebaseAuthentication.net Plugin must be installed.*/
                    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApi.WebAPIKey));
                    try
                    {
                        IsBusy = true;
                        /*It will sends a link to user's email to reset the password.*/
                        await authProvider.SendPasswordResetEmailAsync(Email.Trim().ToLower());
                        await Application.Current.MainPage.DisplayAlert("", "A link has been sent to your email.", "Ok");
                        await Application.Current.MainPage.Navigation.PopAsync();

                        IsBusy = false;
                    }
                    catch (Exception ex)
                    {
                        IsBusy = false;
                        await Application.Current.MainPage.DisplayAlert("", "Invalid email.", "OK");
                    }
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", "Connect your device to internet.", "OK");
            }
        }

    }
}
