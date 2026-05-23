using System.Collections.ObjectModel;
using UniversalYoga.Models;
using UniversalYoga.ViewModels;

namespace UniversalYoga.Views;

public partial class HomePage : ContentPage
{
    public HomeViewModel vm;
    public HomePage()
	{
		InitializeComponent();
		BindingContext = vm = new HomeViewModel();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        vm.courses = new ObservableCollection<YogaCourse>();
        vm.CountItems();
        vm.GetAllCourses();
    }
    private void search_TextChanged(object sender, TextChangedEventArgs e)
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                List.ItemsSource = vm.courses;
            }
            else
            {
                List.ItemsSource = vm.courses.Where(i => i.CourseName.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        });
    }

    private void comboBox_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        if (String.IsNullOrEmpty(vm.data.Day) && String.IsNullOrEmpty(vm.data.time))
        {
            List.ItemsSource = vm.courses;
        }
        else if (!String.IsNullOrEmpty(vm.data.Day) && String.IsNullOrEmpty(vm.data.time))
        {
            vm.isVisibleBtn = true;
            List.ItemsSource = vm.courses.Where(i => i.DayOfWeek.ToLower().Contains(vm.data.Day.ToLower()));
        }
        else if (!String.IsNullOrEmpty(vm.data.Day) && !String.IsNullOrEmpty(vm.data.time))
        {
            vm.isVisibleBtn = true;
            List.ItemsSource = vm.courses.Where(i => i.DayOfWeek.ToLower().Contains(vm.data.Day.ToLower()) && i.TimeOfCourse.ToLower().Contains(vm.data.time.ToLower()));
        }
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        DateTime date = new DateTime(2012, 01, 01);
        TimeSpan ts = new TimeSpan(1, 0, 0, 0, 0);
        date = date + vm.data.Time;
        vm.data.time = date.ToString("h:mm tt");
        if (String.IsNullOrEmpty(vm.data.Day) && String.IsNullOrEmpty(vm.data.time))
        {
            List.ItemsSource = vm.courses;
        }
        else if (String.IsNullOrEmpty(vm.data.Day) && !String.IsNullOrEmpty(vm.data.time))
        {
            vm.isVisibleBtn = true;
            List.ItemsSource = vm.courses.Where(i => i.TimeOfCourse.ToLower().Contains(vm.data.time.ToLower()));
        }
        else if (!String.IsNullOrEmpty(vm.data.Day) && !String.IsNullOrEmpty(vm.data.time))
        {
            vm.isVisibleBtn = true;
            List.ItemsSource = vm.courses.Where(i => i.DayOfWeek.ToLower().Contains(vm.data.Day.ToLower()) && i.TimeOfCourse.ToLower().Contains(vm.data.time.ToLower()));
        }
    }

}