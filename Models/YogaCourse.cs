using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalYoga.ViewModels;

namespace UniversalYoga.Models
{
    public class YogaCourse:BaseViewModel
    {
        private long id; // Unique identifier for each course
        private string dayOfWeek;

        private string courseName;
        private string timeOfCourse;
        private int capacity;
        private string duration;
        private string pricePerClass;
        private string description;
        private string typeOfYoga;

        public string DayOfWeek
        {
            get { return dayOfWeek; }
            set { dayOfWeek = value; OnPropertyChanged(); }
        }

        public string TimeOfCourse
        {
            get { return timeOfCourse; }
            set { timeOfCourse = value; OnPropertyChanged(); }
        }

        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; OnPropertyChanged(); }
        }

        public string Duration
        {
            get { return duration; }
            set { duration = value; OnPropertyChanged(); }
        }

        public string PricePerClass
        {
            get { return pricePerClass; }
            set { pricePerClass = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }

        public string TypeOfYoga
        {
            get { return typeOfYoga; }
            set { typeOfYoga = value; OnPropertyChanged(); }
        }

        public string CourseName
        {
            get { return courseName; }
            set { courseName = value; OnPropertyChanged(); }
        }

        public long Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }
    }
}
