using System.Collections.ObjectModel;
using System.Windows.Input;

namespace UniversalYoga.Views;

public partial class FilterPage : ContentPage
{
    private ObservableCollection<string> _days;

    public ObservableCollection<string> days
    {
        get { return _days; }
        set { _days = value; OnPropertyChanged(); }
    }

    public ICommand BackCmd { get; set; }
    public FilterPage()
	{
		InitializeComponent();
        days = new ObservableCollection<string>();
        days.Add("Monday");
        days.Add("Tuesday");
        days.Add("Wednesday");
        days.Add("Thursday");
        days.Add("Friday");
        days.Add("Saturday");
        days.Add("Sunday");
        BackCmd = new Command(Back);
        BindingContext = this;
    }
    public async void Back()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }
}