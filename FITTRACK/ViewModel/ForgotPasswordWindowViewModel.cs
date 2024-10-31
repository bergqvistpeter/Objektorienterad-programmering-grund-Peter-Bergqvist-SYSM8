using FITTRACK.Model;
using FITTRACK.MVVM;
using FITTRACK.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FITTRACK.ViewModel
{
    class ForgotPasswordWindowViewModel : ViewModelBase
    {   //UserManager
        public User CurrentUser => UserManager.Instance.CurrentUser;
        //Commands
        public RelayCommand SetNewPasswordCommand => new RelayCommand(execute => SetNewPassword());
        public RelayCommand ConfirmSecurityCommand => new RelayCommand(execute => ConfirmSecurity());
        public RelayCommand CancelWindowCommand => new RelayCommand(execute => CancelWindow()); //Command för att avbryta och att stänga fönstret.

        //Egenskaper

        private string inputSecurityAnswer;

        public string InputSecurityAnswer
        {
            get { return inputSecurityAnswer; }
            set
            {
                inputSecurityAnswer = value;
                OnPropertyChanged();
            }
        }

        private string inputPassword;

        public string InputPassword
        {
            get { return inputPassword; }
            set
            {
                inputPassword = value;
                OnPropertyChanged();
            }
        }

        private string inputConfirmPassword;

        public string InputConfirmPassword
        {
            get { return inputConfirmPassword; }
            set
            {
                inputConfirmPassword = value;
                OnPropertyChanged();
            }
        }
        private bool isPasswordsLocked;
        public bool IsPasswordsLocked
        {
            get { return isPasswordsLocked; }
            set
            {
                isPasswordsLocked = value;
                OnPropertyChanged();
            }
        }

        public ForgotPasswordWindowViewModel()

        {
            IsPasswordsLocked = false;
        }

        public void ConfirmSecurity()
        {
            if (inputSecurityAnswer != null)
            {
                if (CurrentUser.SecurityAnswer == InputSecurityAnswer)

                {
                    IsPasswordsLocked = true;
                }
            }
            else
            {
                MessageBox.Show("Inget svar är inmatat, vänligen mata in ett svar", "Error", MessageBoxButton.OK);
            }

        }

        public void SetNewPassword() 
        {
            bool isPasswordValid = InputPassword.Length >= 8 &&  //Variabel för att kontrollera om Passwordet är gilitigt
                  InputPassword.Any(char.IsDigit) &&
                  InputPassword.Any(ch => "!@#$%^&*()_+-={}[]:;\"'<>,.?/\\|~`".Contains(ch)) &&
                  InputPassword.Any(char.IsLetter);

            if (!isPasswordValid) // Kontrulerar att lösenordet innehåller specialtecken
            {
                MessageBox.Show("Lösenordet måste innehålla minst 8 tecken varav minst en(1) bokstav, en(1) siffra och ett(1) specialtecken","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (InputPassword == InputConfirmPassword) 
            { 
                CurrentUser.Password = InputPassword;
                MessageBox.Show("Lösenordet har uppdaterats");
                
                foreach (Window window in Application.Current.Windows)  //går igenom öppna fönster
                {
                    if (window is ForgotPasswordWindow) // om ett fönster som är öppet heter ForgottenPasswordWindow. Stäng det
                    {
                        window.Close();
                        break;
                    }
                }
            }
        }

        private void CancelWindow() //Metod som frågar användaren om de är säkra på att de vill avbryta och om de väljer Yes, så stängs fönstret och MainWindow öppnas.
        {
            MessageBoxResult result = MessageBox.Show("Är du säker på att du vill avrbyta?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {


                foreach (Window window in Application.Current.Windows)  //går igenom öppna fönster
                {
                    if (window is ForgotPasswordWindow) // om ett fönster som är öppet heter ForgottenPasswordWindow. Stäng det
                    {
                        window.Close();
                        break;
                    }
                }

            }
        }
    }
}
