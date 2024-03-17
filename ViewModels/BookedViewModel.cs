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
    public class BookedViewModel : BaseViewModel
    {
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
        public BookedViewModel()
        {
            CurrentState = LayoutState.Loading;
            courses = new ObservableCollection<YogaCourse>();
            _courseService = DependencyService.Resolve<ICourses>();
            _toast = DependencyService.Resolve<IToast>();
            //GetAllCourses();
            //CountItems();
            SelectedCmd = new Command(SelectedItem);
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
                        var list = await _courseService.GetBookedCoursesAsync();
                        if (list == null || list.Count == 0)
                        {
                            CurrentState = LayoutState.Empty;
                        }
                        else
                        {
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
            await Application.Current.MainPage.Navigation.PushAsync(new CourseDetailPage(item));
        }
    }
}
