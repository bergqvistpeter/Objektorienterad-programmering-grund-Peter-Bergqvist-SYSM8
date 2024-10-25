using FITTRACK.MVVM;
using FITTRACK.View;
using System;
using System.Collections.Generic;
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
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        
        public double Weight { get; set; }
       
        public static List<User> Users = new List<User> { new User("user", "password", "Sweden", "Vilket märke hade din första bil?", "Volvo", 100) };

        public string ConfirmPassword;

        
        //Konstruktor
        public User(string username, string password, string country, string securityQuestion, string securityAnswer, double weight) : base(username, password)
        {
            this.Country = country;
            this.SecurityQuestion = securityQuestion;
            this.SecurityAnswer = securityAnswer;
            this.Weight = weight;
        }
        
       
        
        //Metod
        public override void SignIn(string username, string password)
        {
            if (Password == password && Username == username)

            {
                UserManager.Instance.SetCurrentUser(this); //Sätter den inloggade användaren i UserManager


                WorkoutsWindow workoutsWindow = new WorkoutsWindow();
                workoutsWindow.Show();
                Application.Current.MainWindow.Close();
                MessageBox.Show($"Välkommen {Username}","Välkommen!", MessageBoxButton.OK, MessageBoxImage.None);
                

            }

            else
            {
               MessageBox.Show("Felaktigt Password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        public void ResetPassword(string securityAnswer) 
        { 
            
        }

        public double GetUserWeight() 
        {
            return Weight;
        }

        public static void AddUser(User user) 
        { 
            Users.Add(user);
        }
    }
}
