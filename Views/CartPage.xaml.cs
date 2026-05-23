using UniversalYoga.ViewModels;

namespace UniversalYoga.Views;

public partial class CartPage : ContentPage
{
	public CartPage()
	{
		InitializeComponent();
		BindingContext = new CartViewModel();

    }
}