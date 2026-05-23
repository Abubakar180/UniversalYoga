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
        /*This is a model class containing variables of Filter by Day and Time.*/
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

        public TimeSpan Time { get; set; }
    }
}
