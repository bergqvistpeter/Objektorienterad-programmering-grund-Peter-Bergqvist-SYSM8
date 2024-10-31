using FITTRACK.MVVM;
using FITTRACK.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FITTRACK.Model
{
    public class User : Person
    {   //Egenskaper
        public User CurrentUser => UserManager.Instance.CurrentUser;
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        
        public double Weight { get; set; }
        public int UserID { get; set; }
       
       public ObservableCollection<Workout> Workouts { get; set; }

        


public string ConfirmPassword;

        
        //Konstruktor
        public User(string username, string password, string country, string securityQuestion, string securityAnswer, double weight, int userID) : base(username, password)
        {
            this.Country = country;
            this.SecurityQuestion = securityQuestion;
            this.SecurityAnswer = securityAnswer;
            this.Weight = weight;
            this.UserID = userID;

           
        }


        

        //Metod
        public override void SignIn(string username, string password) // Metod för att logga in och öppna WorkoutsWindow och stänga MainWindow
        {
            if (Password == password && Username == username)

            {
                UserManager.Instance.SetCurrentUser(this); //Sätter den inloggade användaren i UserManager


                WorkoutsWindow workoutsWindow = new WorkoutsWindow();
                workoutsWindow.Show();
                Application.Current.MainWindow.Close();
                
                

            }

            else
            {
               MessageBox.Show("Felaktigt Password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        public void ResetPassword(string username) //Metod för att sätta CurrentUser och öppna forgotten Password
        {
            foreach (User user in UserManager.Users)
            {
                if (user.Username == username)
                {
                    UserManager.Instance.SetCurrentUser(this);

                }
            }

            
            ForgotPasswordWindow addWorkoutWindow = new ForgotPasswordWindow(); //Skapar det nya Workout fönstret
            addWorkoutWindow.Show(); //Öppnar det nya workoutfönstret

        }


        public static void AddUser(User user) 
        { 
            UserManager.Users.Add(user);
        }
        public void RemoveWorkout(Workout workout)
        {
            Workouts.Remove(workout);
        }
    }
}
