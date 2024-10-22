using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITTRACK.Model
{
    public class User : Person
    {   //Egenskaper
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        public double Weight { get; set; }
        //Konstruktor
        public User(string username, string password, string country, string securityQuestion, string securityAnswer, double weight) : base(username, password)
        {
            this.Country = country;
            this.SecurityQuestion = securityQuestion;
            this.SecurityAnswer = securityAnswer;
            this.Weight = weight;
        }
        //Metod
        public override void SignIn()
        {
            
        }
        public void ResetPassword(string securityAnswer) 
        { 
            
        }

        public double GetUserWeight() 
        {
            return Weight;
        }
    }
}
