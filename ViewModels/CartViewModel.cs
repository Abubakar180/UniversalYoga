using CommunityToolkit.Maui.Converters;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel.Communication;
using Mopups.Services;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniversalYoga.Models;
using UniversalYoga.Services;
using UniversalYoga.Services.Interface;
using UniversalYoga.Views.IndicatorView;

namespace UniversalYoga.ViewModels
{
    public class CartViewModel: BaseViewModel
    {
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
        private ObservableCollection<YogaCourse> _courses;

        public ObservableCollection<YogaCourse> courses
        {
            get { return _courses; }
            set { _courses = value; OnPropertyChanged(); }
        }
        public SQLiteConnection db;
        private LayoutState _CurrentState;
        public LayoutState CurrentState
        {
            get { return _CurrentState; }
            set { _CurrentState = value; OnPropertyChanged(); }
        }

        private readonly ICourses _courseService;
        private readonly IToast _toast;
        public ICommand BookCmd { get; set; }
        public ICommand BackCmd { get; set; }
        public ICommand DeleteCmd { get; set; }
        public CartViewModel()
        {
            CurrentState = LayoutState.Loading;
            db = Utils.CreateConnection();
            courses = new ObservableCollection<YogaCourse>();
            _courseService = DependencyService.Resolve<ICourses>();
            _toast = DependencyService.Resolve<IToast>();
            GetCartItems();
            BackCmd = new Command(Back);
            BookCmd = new Command(BookNow);
            DeleteCmd = new Command(DeleteItem);
        }
        public async void GetCartItems()
        {
            CurrentState = LayoutState.Loading;
            var email = Preferences.Get("Email", "");
            await Task.Run(async () =>
            {
                var list = ReadOperations.GetAllWithChildren<YogaCourse>(db);
                if (list == null || list.Count == 0)
                {
                    CurrentState = LayoutState.Empty;
                }
                else
                {
                    foreach (var item in list)
                    {
                        if (Device.RuntimePlatform == Device.UWP)
                        {
                            item.IsVisible = true;
                        }
                        if (item.BookedBy == email)
                        {
                            courses.Add(item);
                        }
                    }
                    CurrentState = LayoutState.Success;
                }
            });
        }
        public async void BookNow()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    var email = Preferences.Get("Email", "");
                    if (courses == null || courses.Count() == 0)
                    {
                        await _toast.Show("First add something in cart.");
                    }
                    else
                    {
                        IsBusy = true;
                        var bookedcourses = await _courseService.GetBookedCoursesAsync();
                        if (bookedcourses == null || bookedcourses.Count() == 0)
                        { 
                            bookedcourses = new List<YogaCourse>();
                        }
                        foreach (var course in courses)
                        {
                            var bookeddata = bookedcourses.Where(a => a.Id == course.Id && a.BookedBy == email).FirstOrDefault();
                            if (bookeddata == null)
                            {
                                course.BookedBy = email;
                                await _courseService.BookCourse(course);
                            }
                        }
                        db.DeleteAll<YogaCourse>();
                        //db.DeleteAll<BookbyModel>();
                        courses.Clear();
                        await Application.Current.MainPage.DisplayAlert("", "Courses Booked.", "OK");
                        IsBusy = false;
                    }
                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", "Connect your device to internet.", "OK");
            }
        }
        public async void DeleteItem(object obj)
        {
            var item = obj as YogaCourse;
            var Items = ReadOperations.GetAllWithChildren<YogaCourse>(db);
            var cartitem = Items.Where(a => a.Id == item.Id && a.BookedBy == item.BookedBy).FirstOrDefault();
            db.Delete(cartitem);
            courses.Remove(item);
        }
        public async void Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
