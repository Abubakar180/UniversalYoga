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
}