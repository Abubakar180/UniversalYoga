using UniversalYoga.Views;

namespace UniversalYoga
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent(); 
            Routing.RegisterRoute("CartPage", typeof(CartPage));
            Routing.RegisterRoute("CourseDetailPage", typeof(CourseDetailPage));
        }
    }
}
