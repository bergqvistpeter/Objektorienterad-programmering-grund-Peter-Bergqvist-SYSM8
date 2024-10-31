using FITTRACK.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        // Metod för att hämta alla träningar
        public ObservableCollection<Workout> GetAllWorkouts()
        {
            ObservableCollection<Workout> allWorkouts = new ObservableCollection<Workout>();

            foreach (User user in UserManager.Users)
            {
                foreach (var workout in user.Workouts)
                {
                    allWorkouts.Add(workout); // Lägg till varje träning till ObservableCollection
                }
            }

            return allWorkouts;
        }

        public void ManageAllWorkouts() { } //Inte haft någon navändning för denna metoden, då jag löst det på annat sätt.
        
    }
}
