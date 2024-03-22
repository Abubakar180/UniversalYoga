using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalYoga.ViewModels;

namespace UniversalYoga.Models
{
    public class FilterModel:BaseViewModel
    {
        private string _Day;

        public string Day
        {
            get { return _Day; }
            set { _Day = value; OnPropertyChanged(); }
        }
        private string _time;

        public string time
        {
            get { return _time; }
            set { _time = value; OnPropertyChanged(); }
        }

        //public string Day { get; set; }
        public TimeSpan Time { get; set; }
        //public string time { get; set; }
    }
}
