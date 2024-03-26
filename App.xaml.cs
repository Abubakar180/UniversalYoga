using SQLite;
using UniversalYoga.Models;
using UniversalYoga.Services.Implementation;
using UniversalYoga.Services.Interface;
using UniversalYoga.Views;

namespace UniversalYoga
{
    /*This is the starting point of application. 
     Cross-platform application lifecycle events can raised be here.*/
    public partial class App : Application
    {
        /*This is sqlite service created in Services folder named Utils.
         For this SQLiteNetExtensions plugin must be installed from the nuget package.*/
        public SQLiteConnection db;
        public App()
        {
            /*Registering the Syncfusion License Key for using Syncfusion controls like dropdown combobox, time picker etc. 
              The License Key is generated based on the version of controls.
              The Licensing key can be taken from Syncfusion website by login or registering account.*/
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjgwODkxNUAzMjMzMmUzMDJlMzBBMlNESW9YanNjejYxQldPbVFBVHZ6dUUvZWtzMjNyekZWTjdLWVlJK2JzPQ ==");
            /*The InitializeComponent method that's called from the constructor and adds it to the compilation object.*/
            InitializeComponent();
            /*Here is the sqlite connection is created to where you want to save the data.*/
            db = Services.Utils.CreateConnection();
            /*After the sqlite connection is created, you can create table.*/
            db.CreateTable<YogaCourse>();
            /*With dependency injection, another class is responsible for injecting dependencies into an object at runtime.*/
            DependencyService.Register<IToast, ToastMessage>();
            DependencyService.Register<IUser, UserService>();
            DependencyService.Register<ICourses, CourseService>();
            DependencyService.Register<IClasses, ClassService>();
            /*Preferences are stored with a String key. It is used to store string.
             To retrieve Preferences Get Function is called against String key.*/
            var login = Preferences.Get("Login", "default");
            if (login == "default")
            {
                /*This method is used to set default position and size of windows app.*/
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                /*This method is used to set default position and size of windows app.*/
                MainPage = new AppShell();
            }
        } 
        /*This method is used to set default position and size of windows app.*/
        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            const int newWidth = 800;
            const int newHeight = 750;

            window.X = 500;
            window.Y = 200;

            window.MinimumHeight = newHeight;
            window.MinimumWidth = newWidth;

            return window;
        }
    }
}
