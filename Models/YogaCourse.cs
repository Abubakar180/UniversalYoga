using Microsoft.Maui.Graphics.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalYoga.ViewModels;

namespace UniversalYoga.Models
{
    /*This is a model class containing variables of Courses info.*/

    /*A sqlite table is declared here.*/
    [Table("CourseTable")]
    public class YogaCourse:BaseViewModel
    {
        /*PrimaryKey is declared for each course saved in local database.*/
        [PrimaryKey, AutoIncrement]
        public int courseID { get; set; }
        public string Key { get; set; }
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

        private bool _isCart;
        public bool isCart
        {
            get { return _isCart; }
            set
            {
                _isCart = value;
                OnPropertyChanged();
            }
        }

        private bool _IsVisible;
        public bool IsVisible
        {
            get { return _IsVisible; }
            set
            {
                _IsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _Booked;
        public bool Booked
        {
            get { return _Booked; }
            set
            {
                _Booked = value;
                OnPropertyChanged();
            }
        }
        private string _status;

        public string status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(); }
        }
        private string _BookedBy;

        public string BookedBy
        {
            get { return _BookedBy; }
            set { _BookedBy = value; OnPropertyChanged(); }
        }
    }
}
