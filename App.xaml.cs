using SQLite;
using UniversalYoga.Models;
using UniversalYoga.Services.Implementation;
using UniversalYoga.Services.Interface;
using UniversalYoga.Views;

namespace UniversalYoga
{
    public partial class App : Application
    {
        public SQLiteConnection db;
        public App()
        {
            InitializeComponent();
            db = Services.Utils.CreateConnection();
            //db.DeleteAll<YogaCourse>();
            //db.DeleteAll<BookbyModel>();
            db.CreateTable<YogaCourse>();
            //db.CreateTable<BookbyModel>();
            DependencyService.Register<IToast, ToastMessage>();
            DependencyService.Register<IUser, UserService>();
            DependencyService.Register<ICourses, CourseService>();
            DependencyService.Register<IClasses, ClassService>();
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
