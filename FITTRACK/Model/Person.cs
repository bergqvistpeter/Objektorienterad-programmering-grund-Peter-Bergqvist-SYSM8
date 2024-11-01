using FITTRACK.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITTRACK.Model
{
    public abstract class Person : ViewModelBase
    {   //Egenskaper
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password { get; set; }
        //Konstruktor
        public Person (string username, string password) 
        { 
            this.Username = username;
            this.Password = password;
        
        }
        //Metod
        public abstract void SignIn(string username, string password);
    }
}
