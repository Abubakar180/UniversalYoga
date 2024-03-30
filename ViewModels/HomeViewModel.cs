using CommunityToolkit.Maui.Converters;
using Mopups.Services;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniversalYoga.Models;
using UniversalYoga.Services;
using UniversalYoga.Services.Interface;
using UniversalYoga.Views;
using UniversalYoga.Views.IndicatorView;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UniversalYoga.ViewModels
{
    public class HomeViewModel: BaseViewModel
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
        private ObservableCollection<YogaCourse> _filteredcourses;

        public ObservableCollection<YogaCourse> filteredcourses
        {
            get { return _filteredcourses; }
            set { _filteredcourses = value; OnPropertyChanged(); }
        }
        private ObservableCollection<YogaCourse> _courses;

        public ObservableCollection<YogaCourse> courses
        {
            get { return _courses; }
            set { _courses = value; OnPropertyChanged(); }
        }
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(); }
        }
        private string _searchTxt;

        public string searchTxt
        {
            get { return _searchTxt; }
            set { _searchTxt = value; OnPropertyChanged(); }
        }

        private LayoutState _CurrentState;
        public LayoutState CurrentState
        {
            get { return _CurrentState; }
            set { _CurrentState = value; OnPropertyChanged(); }
        }
        private int _count;

        public int Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged(); }
        }
        private bool _visible;

        public bool visible
        {
            get { return _visible; }
            set { _visible = value; OnPropertyChanged(); }
        }
        private bool _isVisibleBtn;

        public bool isVisibleBtn
        {
            get { return _isVisibleBtn; }
            set { _isVisibleBtn = value; OnPropertyChanged(); }
        }
        private bool _isExpanded;

        public bool isExpanded
        {
            get { return _isExpanded; }
            set { _isExpanded = value; OnPropertyChanged(); }
        }
        private FilterModel _data;

        public FilterModel data
        {
            get { return _data; }
            set { _data = value; OnPropertyChanged(); }
        }
        private ObservableCollection<string> _days;

        public ObservableCollection<string> days
        {
            get { return _days; }
            set { _days = value; OnPropertyChanged(); }
        }
        public SQLiteConnection db;
        private readonly ICourses _courseService;
        private readonly IToast _toast;
        public ICommand ViewcartCmd { get; set; }
        public ICommand cartCmd { get; set; }
        public ICommand logoutCmd { get; set; }
        public ICommand SelectedCmd { get; set; }
        public ICommand RemoveFilterCmd { get; set; }
        #endregion

        #region Constructor
        public HomeViewModel()
        {
            isVisibleBtn = false;
            CurrentState = LayoutState.Loading;
            Name = Preferences.Get("Name", "");
            db = Utils.CreateConnection();
            data = new FilterModel();
            filteredcourses = new ObservableCollection<YogaCourse>();
            courses = new ObservableCollection<YogaCourse>();
            _courseService = DependencyService.Resolve<ICourses>();
            _toast = DependencyService.Resolve<IToast>();
            //GetAllCourses();
            //CountItems();
            days = new ObservableCollection<string>();
            days.Add("Monday");
            days.Add("Tuesday");
            days.Add("Wednesday");
            days.Add("Thursday");
            days.Add("Friday");
            days.Add("Saturday");
            days.Add("Sunday");
            ViewcartCmd = new Command(ViewCart);
            cartCmd = new Command(AddtoCart);
            logoutCmd = new Command(Logout);
            SelectedCmd = new Command(SelectedItem);
            RemoveFilterCmd = new Command(RemoveFilter);
        }
        #endregion

        #region Functions
        /*This Functions Counts the number of items in cart. 
         Number will be invisible if there is not a single item in cart*/
        public async void CountItems()
        {
            var email = Preferences.Get("Email", "");
            Count = 0;
            var Items = ReadOperations.GetAllWithChildren<YogaCourse>(db);
            if (Items == null || Items.Count() == 0)
            {
                visible = false;
            }
            else
            {
                foreach (var item in Items)
                {
                    if (item.BookedBy == email)
                    {
                        Count = Count + 1;
                    }
                }
                visible = true;
            }
            if (Count == 0)
            {
                visible = false;
            }
        }

        /*Requesting for storage permission to save the data in local DB.*/
        public async void RequestForPermission()
        {
            try
            {
                var Status = PermissionStatus.Unknown;
                Status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                if (Status == PermissionStatus.Granted)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new CartPage());
                }
                else
                    Status = await Permissions.RequestAsync<Permissions.StorageRead>();

                if (Permissions.ShouldShowRationale<Permissions.StorageRead>())
                {
                    await Application.Current.MainPage.DisplayAlert("", "App needs storage permission", "OK");
                }

                if (Status != PermissionStatus.Granted)
                {
                    Status = await Permissions.RequestAsync<Permissions.StorageRead>();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public async void ViewCart()
        {
            RequestForPermission();
        }

        /*This Function will run if user taps on add to cart.*/
        public async void AddtoCart(object obj)
        {
            try
            {
                IsBusy = true;
                var item = obj as YogaCourse;
                var email = Preferences.Get("Email", "");
                var Items = ReadOperations.GetAllWithChildren<YogaCourse>(db);
                var cart_item = Items.Where(a => a.Id == item.Id && a.BookedBy == email).FirstOrDefault();
                if (cart_item == null)
                {
                    item.isCart = false;
                    item.Booked = true;
                    item.BookedBy = Preferences.Get("Email", "");
                    item.status = "In Cart";
                    db.InsertWithChildren(item);
                    CountItems();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("", "Item already in cart.", "OK");
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
        }
        /*This Function will run if user taps on logout.*/
        public async void Logout()
        {
            /*To remove Preferences and goto to the Login Page*/
            Preferences.Remove("Email", "");
            Preferences.Remove("Address", "");
            Preferences.Remove("Contact", "");
            Preferences.Remove("Name", "");
            Preferences.Remove("Login", "");
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
        public async void GetAllCourses()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    CurrentState = LayoutState.Loading;
                    var email = Preferences.Get("Email", "");
                    await Task.Run(async () =>
                    {
                        var list = await _courseService.GetAllCoursesAsync();
                        if (list == null || list.Count == 0)
                        {
                            CurrentState = LayoutState.Empty;
                        }
                        else
                        {
                            var booked_list = await _courseService.GetBookedCoursesAsync();
                            if (booked_list == null || booked_list.Count == 0)
                            {
                                booked_list = new List<YogaCourse>();
                            }
                            var cartItems = ReadOperations.GetAllWithChildren<YogaCourse>(db);
                            if (cartItems == null || cartItems.Count == 0)
                            {
                                cartItems = new List<YogaCourse>();
                            }
                            foreach (var item in list)
                            {
                                /*First it will check whether course is booked or not.
                                 By matching the course id and email*/
                                var bookedCourse = booked_list.Where(a => a.Id == item.Id && a.BookedBy == email).FirstOrDefault();
                                /*If it does retrive the booked course*/
                                if (bookedCourse == null)
                                {
                                    /*Then it will check whether course is in the cart or not.*/
                                    var course = cartItems.Where(a => a.Id == item.Id && a.BookedBy == email).FirstOrDefault();
                                    /*If it does retrive the cart course*/
                                    if (course == null)
                                    {
                                        /*Then add to cart button will be visible.*/
                                        item.isCart = true;
                                        item.Booked = false;
                                    }
                                    /*If it retrives the cart course*/
                                    else
                                    {
                                        /*Then the item status will be In Cart and add to cart button will be invisible.*/
                                        item.status = "In Cart";
                                        item.isCart = false;
                                        item.Booked = true;
                                    }
                                }
                                /*If it retrives the booked course*/
                                else
                                {
                                    /*Then the item status will be Booked and add to cart button will be invisible.*/
                                    item.status = "Booked";
                                    item.isCart = false;
                                    item.Booked = true;
                                }
                                courses.Add(item);
                            }
                            CurrentState = LayoutState.Success;
                        }
                    });
                }
                catch (Exception ex)
                {
                    CurrentState = LayoutState.Empty;
                    await Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", "Connect your device to internet.", "OK");
            }

        }
        public async void SelectedItem(object obj)
        {
            var item = obj as YogaCourse;
            searchTxt = ""; 
            isVisibleBtn = false;
            isExpanded = false;
            data = new FilterModel();
            /*The selected item will be passed to CourseDetailPage as parameter.*/
            await Application.Current.MainPage.Navigation.PushAsync(new CourseDetailPage(item));
        }
        /*This Function will run if user taps on remove filter button.*/
        public async void RemoveFilter()
        {
            isVisibleBtn = false;
            isExpanded = false;
            data = new FilterModel();
            courses = new ObservableCollection<YogaCourse>();
            GetAllCourses();
        }
        #endregion
    }
}
