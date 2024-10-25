using FITTRACK.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITTRACK.Model
{
    public class UserManager : ViewModelBase
    {
        private static UserManager instance;
        public static UserManager Instance => instance ??= new UserManager();

        public User CurrentUser { get; private set; }

        private UserManager()
        {
            
        }

        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
            
        }

    }
}
