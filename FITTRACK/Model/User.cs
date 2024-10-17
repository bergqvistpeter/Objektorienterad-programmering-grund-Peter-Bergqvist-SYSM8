using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITTRACK.Model
{
    class User : Person
    {
        public string Country;
        public string SecurityQuestion;
        public string SecurityAnswer;

        public User(string username, string password, string country, string securityQuestion, string securityAnswer) : base(username, password)
        {
            this.Country = country;
            this.SecurityQuestion = securityQuestion;
            this.SecurityAnswer = securityAnswer;
        }

        public override void SignIn()
        {
            
        }
        public void ResetPassword(string securityAnswer) { }
    }
}
