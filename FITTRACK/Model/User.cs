using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITTRACK.Model
{
    class User : Person
    {
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        public double Weight { get; set; }

        public User(string username, string password, string country, string securityQuestion, string securityAnswer, double weight) : base(username, password)
        {
            this.Country = country;
            this.SecurityQuestion = securityQuestion;
            this.SecurityAnswer = securityAnswer;
            this.Weight = weight;
        }

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
