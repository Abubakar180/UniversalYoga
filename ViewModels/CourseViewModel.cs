using CommunityToolkit.Maui.Converters;
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
    public class CourseViewModel : BaseViewModel
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
        private ObservableCollection<YogaClass> _classes;

        public ObservableCollection<YogaClass> classes
        {
            get { return _classes; }
            set { _classes = value; OnPropertyChanged(); }
        }

        private YogaCourse _course;

        public YogaCourse course
        {
            get { return _course; }
            set { _course = value; OnPropertyChanged(); }
        }
        public SQLiteConnection db;

        private readonly IClasses _classService;
        private readonly IToast _toast;
        public ICommand BackCmd { get; set; }
        public ICommand CartCmd { get; set; }
        public CourseViewModel(YogaCourse model)
        {
            db = Utils.CreateConnection();
            course = new YogaCourse();
            course = model;
            classes = new ObservableCollection<YogaClass>();
            _classService = DependencyService.Resolve<IClasses>();
            _toast = DependencyService.Resolve<IToast>();
            GetClasses();
            BackCmd = new Command(Back);
            CartCmd = new Command(AddtoCart);
        }
        public async void AddtoCart()
        {
            try
            {
                IsBusy = true;
                course.isCart = false;
                course.Booked = true;
                course.status = "In Cart";
                var Items = ReadOperations.GetAllWithChildren<YogaCourse>(db);
                var cart_item = Items.Where(a => a.Id == course.Id).FirstOrDefault();
                if (cart_item == null)
                {
                    db.InsertWithChildren(course);
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

        public async void GetClasses()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    IsBusy = true;
                    var list = await _classService.GetAllClassesAsync();
                    if (list == null || list.Count == 0)
                    {
                        IsBusy = false;
                    }
                    else
                    {
                        foreach (var item in list)
                        {
                            if (course.Id == item.CourseId)
                            {
                                classes.Add(item);
                            }
                        }
                    }
                    IsBusy = false;
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
        public async void Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
