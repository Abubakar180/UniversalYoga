using UniversalYoga.ViewModels;

namespace UniversalYoga.Views;

public partial class LoginPage : ContentPage
{
    public LoginViewModel vm;
    public LoginPage()
	{
		InitializeComponent();
		BindingContext = vm = new LoginViewModel();
    }

    private void login_Clicked(object sender, EventArgs e)
    {
        vm.Login();
    }
}