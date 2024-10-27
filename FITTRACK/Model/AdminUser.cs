using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITTRACK.Model
{
    class AdminUser : User
    {   //Konstruktor
        public AdminUser(string username, string password, string country, string securityQuestion, string securityAnswer, double weight, int userID) : base(username, password, country, securityQuestion, securityAnswer, weight, userID)
        {
        
        }
        //Metod
        public void ManageAllWorkouts() 
        { 
        
        }
    }
}
