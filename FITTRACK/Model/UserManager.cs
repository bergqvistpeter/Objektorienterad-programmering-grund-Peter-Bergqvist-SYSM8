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
        public User CurrentUser;


		private ObservableCollection<User> loggedInUsers;		

		public ObservableCollection<User> LoggedInUsers
		{
			get { return loggedInUsers; }
			set 
			{ 
				loggedInUsers = value;
				OnPropertyChanged();
			}
		}

		public UserManager() 
		{
            loggedInUsers = new ObservableCollection<User>();
        }

        public void AddUser(User user)
        {
            if (!loggedInUsers.Contains(user))
            {
                loggedInUsers.Add(user);
                OnPropertyChanged();
                CurrentUser = user;
            }
        }

        public void RemoveUser(User user)
        {
            if (loggedInUsers.Contains(user))
            {
                loggedInUsers.Remove(user);
                OnPropertyChanged();
            }
        }
    }
}
