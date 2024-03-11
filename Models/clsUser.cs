using UniversalYoga.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalYoga.Models
{
    public class clsUser : BaseViewModel
    {
        public string key { get; set; }
        private string _fName;
        public string FirstName
        {
            get { return _fName; }
            set { _fName = value; OnPropertyChanged(); }
        }
        private string _lName;
        public string LastName
        {
            get { return _lName; }
            set { _lName = value; OnPropertyChanged(); }
        }
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; OnPropertyChanged(); }
        }
        private string _password;
        public string password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }
        private string _Contact;
        public string Contact
        {
            get { return _Contact; }
            set { _Contact = value; OnPropertyChanged(); }
        }
        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; OnPropertyChanged(); }
        }
    }
}
