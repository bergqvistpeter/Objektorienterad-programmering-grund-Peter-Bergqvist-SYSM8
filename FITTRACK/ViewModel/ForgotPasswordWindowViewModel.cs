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
        public RelayCommand SetNewPasswordCommand => new RelayCommand(execute => SetNewPassword()); //Updaterar ditt password
        public RelayCommand ConfirmSecurityCommand => new RelayCommand(execute => ConfirmSecurity()); //Kontrollerar om svaret är rätt på security question
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
            IsPasswordsLocked = false; // Sätter värdet till password delen IsEnabled.
        }
        //Metoder
        public void ConfirmSecurity() //Metod som kontrollerar att lösenordet är rätt.
        {
            if (inputSecurityAnswer != null) // Kollar så att där är ett svar inmatat
            {
                if (CurrentUser.SecurityAnswer == InputSecurityAnswer) // Om svaret stämmer överrens så sätts Passowrdboxarna till IsEnabled=True

                {
                    IsPasswordsLocked = true;
                }
            }
            else
            {
                MessageBox.Show("Inget svar är inmatat, vänligen mata in ett svar", "Error", MessageBoxButton.OK); //Om inget är inmatat får man ett felmeddelande
            }

        }

        public void SetNewPassword() // Metod som kontrollerar inmatade passwordet, att det är korrekt
        {
            bool isPasswordValid = InputPassword.Length >= 8 &&  //Variabel för att kontrollera om Passwordet är gilitigt
                  InputPassword.Any(char.IsDigit) &&
                  InputPassword.Any(ch => "!@#$%^&*()_+-={}[]:;\"'<>,.?/\\|~`".Contains(ch)) &&
                  InputPassword.Any(char.IsLetter);
            if (string.IsNullOrWhiteSpace(InputPassword) || string.IsNullOrWhiteSpace(inputConfirmPassword)) //Kontrollerar så ass båda löserordsrutorna är ifyllda
                
            {
                MessageBox.Show("Båda lösenorden är inte ifyllda");
                return;
            }

            if (!isPasswordValid) // Kontrollerar att lösenordet innehåller alla variabler som krävs
            {
                MessageBox.Show("Lösenordet måste innehålla minst 8 tecken varav minst en(1) bokstav, en(1) siffra och ett(1) specialtecken","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (InputPassword == InputConfirmPassword)  //Jämför passwordboxarna
            { 
                CurrentUser.Password = InputPassword; //Sätter det nya passwordet
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
