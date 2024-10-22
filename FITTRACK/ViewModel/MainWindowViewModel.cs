using FITTRACK.Model;
using FITTRACK.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITTRACK.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand SignIn => new RelayCommand(execute => SignIn(Password,Username));

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

        
    }
}
