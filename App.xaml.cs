using UniversalYoga.Services.Implementation;
using UniversalYoga.Services.Interface;
using UniversalYoga.Views;

namespace UniversalYoga
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<IToast, ToastMessage>();
            DependencyService.Register<IUser, UserService>();
            //Preferences.Remove("Email", "");
            var login = Preferences.Get("Login", "default");
            if (login == "default")
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new AppShell();
            }
        }
    }
}
