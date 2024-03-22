using Syncfusion.Maui.Picker;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UniversalYoga.Services.Interface;

namespace UniversalYoga.Views;

public partial class FilterPage : ContentPage
{
    //private ObservableCollection<string> _days;

    //public ObservableCollection<string> days
    //{
    //    get { return _days; }
    //    set { _days = value; OnPropertyChanged(); }
    //}
    //private string _Day;

    //public string Day
    //{
    //    get { return _Day; }
    //    set { _Day = value; OnPropertyChanged(); }
    //}
    //public FilterModel filter { get; set; }
    //private readonly IToast _toast;
    //public ICommand BackCmd { get; set; }
    //public ICommand SaveCmd { get; set; }
    public FilterPage()
	{
		InitializeComponent(); 
        //filter = new FilterModel();
        //days = new ObservableCollection<string>();
        //_toast = DependencyService.Resolve<IToast>();
        //days.Add("Monday");
        //days.Add("Tuesday");
        //days.Add("Wednesday");
        //days.Add("Thursday");
        //days.Add("Friday");
        //days.Add("Saturday");
        //days.Add("Sunday");

        //BackCmd = new Command(Back);
        //SaveCmd = new Command(Save);
        BindingContext = this;
    }
    //public async void Save()
    //{
    //    if (String.IsNullOrEmpty(filter.Day) && filter.Time== new TimeSpan(00, 00, 00))
    //    {
    //        await _toast.Show("Please select filter by Day or Time.");
    //    }
    //    else
    //    {
    //        DateTime date = new DateTime(2012, 01, 01);
    //        TimeSpan ts = new TimeSpan(1, 0, 0, 0, 0);
    //        date = date + filter.Time;
    //        filter.time = date.ToString("h:mm tt");
    //        MessagingCenter.Send<FilterModel>(filter, "GetitemsbyDayTime");
    //        await Application.Current.MainPage.Navigation.PopAsync();
    //    }
    //}
    //public async void Back()
    //{
    //    await Application.Current.MainPage.Navigation.PopAsync();
    //}
}