using FITTRACK.Model;
using FITTRACK.MVVM;
using FITTRACK.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FITTRACK.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {   //Commands
        public RelayCommand SignInCommand => new RelayCommand(execute => SignIn());
        public RelayCommand OpenRegisterWindowCommand => new RelayCommand(execute => OpenRegisterWindow()); //Command som öppnar nytt fönster
        public RelayCommand CloseProgramWindowCommand => new RelayCommand(execute => CloseProgramWindow()); //Command som stänger programmet

        
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

        private void SignIn()
        {
            
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
