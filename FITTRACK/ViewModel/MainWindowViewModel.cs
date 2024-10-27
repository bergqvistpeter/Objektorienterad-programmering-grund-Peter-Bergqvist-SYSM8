using FITTRACK.Model;
using FITTRACK.MVVM;
using FITTRACK.View;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FITTRACK.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {   //Commands
        public RelayCommand SignInCommand => new RelayCommand(execute => SignIn()); //Command som logga in dig
        public RelayCommand OpenRegisterWindowCommand => new RelayCommand(execute => OpenRegisterWindow()); //Command som öppnar nytt fönster
        public RelayCommand CloseProgramWindowCommand => new RelayCommand(execute => CloseProgramWindow()); //Command som stänger programmet

        public User CurrentUser => UserManager.Instance.CurrentUser;

        public string username;
        public string Username {
            get
            { 
                return username; 
            }
            set 
            {
                username = value;
                OnPropertyChanged();
            } 
        }

        private string password;

        public string Password
        {
            get 
            { 
                return password; 
            }
            set 
            { 
                
                password = value;
                OnPropertyChanged();
            
            }
        }

        public MainWindowViewModel() 
        {
           
        }
     
        

        private void SignIn() //Metod för att logga in
        {
            foreach (User user in UserManager.Users) //Går igenom lista med Användare och jämför ANvändarnamn
            {
                if (user.Username == Username)
                {
                    user.SignIn(Username, Password);
                   
                    return;
                }
               
            }
            {
                MessageBox.Show("Felaktigt Username", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenRegisterWindow() // Metod som öppnar Register fönster och stänger det gamla
        {
            RegisterWindow registerWindow = new RegisterWindow(); //Skapar det nya Register fönstret
            registerWindow.Show(); //Öppnar det nya Register fönstret
            Application.Current.MainWindow.Close(); //Stänger huvudfönstret
            
        }

        private void CloseProgramWindow() 
        {
            Application.Current.Shutdown(); //Stänger programmet
        }
    }
}
