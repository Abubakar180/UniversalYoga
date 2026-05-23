using UniversalYoga.ViewModels;

namespace UniversalYoga.Views;

public partial class ForgotPasswordPage : ContentPage
{
	public ForgotPasswordPage()
	{
		InitializeComponent();
		BindingContext = new ForgotPasswordViewModel();
    }
}