using UniversalYoga.Models;
using UniversalYoga.ViewModels;

namespace UniversalYoga.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterViewModel vm;
    public RegisterPage()
	{
		InitializeComponent();
        BindingContext =vm = new RegisterViewModel();
	}

    private void signup_Clicked(object sender, EventArgs e)
    {
        vm.SignupAsync();
    }
}