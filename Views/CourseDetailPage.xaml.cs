using UniversalYoga.Models;
using UniversalYoga.ViewModels;

namespace UniversalYoga.Views;

public partial class CourseDetailPage : ContentPage
{
	public CourseViewModel vm;
    public CourseDetailPage(YogaCourse model)
	{
		InitializeComponent();
		BindingContext = vm = new CourseViewModel(model);
    }
}