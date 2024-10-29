using FITTRACK.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITTRACK.Model
{
    public class UserManager : ViewModelBase
    {
        private static UserManager instance;
        public static UserManager Instance => instance ??= new UserManager();
        //Lista med användare
        public static List<User> Users = new List<User> { new User("user", "password", "Sweden", "Vilket märke hade din första bil?", "Volvo", 100, 1)
    {
        Workouts = new ObservableCollection<Workout>
        {
            new CardioWorkout
            {
                Date = DateTime.Now,
                Type = "Cardio",
                Duration = new TimeSpan(0, 30, 0),
                CaloriesBurned = 300,
                Notes = "Löpbandet"
            },
            new StrengthWorkout
            {
                Date = DateTime.Now,
                Type = "Strength",
                Duration = new TimeSpan(1, 0, 0),
                CaloriesBurned = 300,
                Notes = "Överkropp"
            }
        }
    },
    new User("admin", "password", "Sweden", "Vilket märke hade din första bil?", "Volvo", 100, 2)
    
    };


        public User CurrentUser { get; private set; }

        public UserManager()
        {

            

        }

        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
            
        }
        
    }
}
