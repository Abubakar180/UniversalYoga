using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalYoga.ViewModels;

namespace UniversalYoga.Models
{
    public class YogaClass : BaseViewModel
    {
        public string Key { get; set; }
        private long id;
        private long courseId;
        private string date;
        private string teacherName;
        private string description;
        public long Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }
        public string Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(); }
        }
        public string TeacherName
        {
            get { return teacherName; }
            set { teacherName = value; OnPropertyChanged(); }
        }
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }

        public long CourseId
        {
            get { return courseId; }
            set { courseId = value; OnPropertyChanged(); }
        }
    }
}
