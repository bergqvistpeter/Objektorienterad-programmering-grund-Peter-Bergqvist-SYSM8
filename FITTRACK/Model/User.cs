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

        public User(string username, string password) : base(username, password)
        {
        }

        public override void SignIn()
        {
            if () { }
        }
    }
}
