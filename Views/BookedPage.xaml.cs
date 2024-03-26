using System.Collections.ObjectModel;
using UniversalYoga.Models;
using UniversalYoga.ViewModels;

namespace UniversalYoga.Views;

public partial class BookedPage : ContentPage
{
    //This is Backend of BookedPage, it's Binding context setted with BookedViewModel
	public BookedViewModel vm;

    public BookedPage()
	{
		InitializeComponent();
		BindingContext = vm = new BookedViewModel();
    }
    //OnAppearing calls every time this page appears and runs the method implemented in BookedViewModel.
    protected override void OnAppearing()
    {
        base.OnAppearing();
        vm.courses = new ObservableCollection<YogaCourse>();
        vm.GetAllCourses();
    }
    //Entry Text Changed Event by searching courses by name.
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