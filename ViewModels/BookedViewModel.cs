using CommunityToolkit.Maui.Converters;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UniversalYoga.Models;
using UniversalYoga.Services.Interface;
using UniversalYoga.Views;

namespace UniversalYoga.ViewModels
{
    /*The Model-View-ViewModel (MVVM) pattern enforces a separation 
     between three software layers — the XAML user interface, called the view, 
    the underlying data, called the model, and 
    an intermediary between the view and the model, called the viewmodel.
    It is responsible for interaction between Views and Models.*/
    public class BookedViewModel : BaseViewModel
    {
        #region
        private ObservableCollection<YogaCourse> _courses;

        public ObservableCollection<YogaCourse> courses
        {
            get { return _courses; }
            set { _courses = value; OnPropertyChanged(); }
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
        private readonly ICourses _courseService;
        private readonly IToast _toast;
        public ICommand SelectedCmd { get; set; }
        #endregion
        /*In the construct variables must be initialized declared outside of construct.*/
        public BookedViewModel()
        {
            CurrentState = LayoutState.Loading;
            courses = new ObservableCollection<YogaCourse>();
            /*In the construct variables must be initialized declared outside of construct.*/
             /*After a type is registered, it can be resolved or injected as a dependency.
              When a type is being resolved, and the container needs to create a new instance,
             it injects any dependencies into the instance.*/
            _courseService = DependencyService.Resolve<ICourses>();
            _toast = DependencyService.Resolve<IToast>();
            SelectedCmd = new Command(SelectedItem);
        }
        public async void GetAllCourses()
        {
            /*Checks if device is connected to internet?.*/
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                try
                {
                    /*StateLayout bound with front end layout.
                      The front Layouts are changes when state changes.*/
                    CurrentState = LayoutState.Loading;
                    var email = Preferences.Get("Email", "");
                    await Task.Run(async () =>
                    {
                        var list = await _courseService.GetBookedCoursesAsync();
                        if (list == null || list.Count == 0)
                        {
                            CurrentState = LayoutState.Empty;
                        }
                        else
                        {
                            /*Looping through each item in list.*/
                            foreach (var item in list)
                            {
                                if (item.BookedBy == email)
                                {
                                    courses.Add(item);
                                }
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
            /*Selects the item from list and pass it to CourseDetailPage as parameter.*/
            await Application.Current.MainPage.Navigation.PushAsync(new CourseDetailPage(item));
        }
    }
}
