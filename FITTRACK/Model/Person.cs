using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITTRACK.Model
{
    public abstract class Person
    {   //Egenskaper
        public string Username;
        public string Password;
        //Konstruktor
        public Person (string username, string password) 
        { 
            this.Username = username;
            this.Password = password;
        
        }
        //Metod
        public abstract void SignIn();
    }
}
