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
        public RelayCommand ForgotPasswordCommand => new RelayCommand(execute => ForgotPassword()); //Command som skapar en 2FA kod
        public RelayCommand Generate2FACommand => new RelayCommand(execute => Generate2FA()); //Command som skapar en 2FA kod
        public RelayCommand SignInCommand => new RelayCommand(execute => SignIn()); //Command som logga in dig
        public RelayCommand OpenRegisterWindowCommand => new RelayCommand(execute => OpenRegisterWindow()); //Command som öppnar nytt fönster
        public RelayCommand CloseProgramWindowCommand => new RelayCommand(execute => CloseProgramWindow()); //Command som stänger programmet
        //UserManager
        public User CurrentUser => UserManager.Instance.CurrentUser;
        //Egenskaper
        public string username;
        public string Username
        {
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

        private string authentication;

        public string Authentication
        {
            get { return authentication; }
            set
            {
                authentication = value;
                OnPropertyChanged();
            }
        }

        private string Temp2FA;

        public MainWindowViewModel()
        {

        }



        private void SignIn() //Metod för att logga in
        {
            foreach (User user in UserManager.Users) //Går igenom lista med Användare och jämför ANvändarnamn
            {
                if (user.Username == Username)
                {
                    if (Authentication == Temp2FA)
                    {
                        user.SignIn(Username, Password);

                        return;
                    }
                    else
                    {
                        MessageBox.Show("Felaktigt 2FA Authentication", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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

        private void Generate2FA()
        {
            Random random = new Random();
            int number = random.Next(0, 1000000); // Genererar ett tal mellan 0 och 999999
            Temp2FA = number.ToString("D6"); // Formaterar talet som 6 siffror med ledande nollor
            MessageBox.Show($"Ett mail har skickats med din authentication kod ({Temp2FA})", "2FAAuthentication", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void ForgotPassword()
        {
            if (!string.IsNullOrEmpty(Username))

            {

                foreach (User user in UserManager.Users)
                {
                    if (user.Username == Username)
                    {
                        user.ResetPassword(Username);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Användaren finns inte", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }


                }
            }
            else    
            {
                MessageBox.Show("Vänligen fyll i Username på den användaren som flömt lösenordet", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

