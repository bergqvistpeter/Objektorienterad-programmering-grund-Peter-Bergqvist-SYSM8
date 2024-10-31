using FITTRACK.Model;
using FITTRACK.MVVM;
using FITTRACK.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FITTRACK.ViewModel
{
    public class RegisterWindowViewModel : ViewModelBase
    {
        //Commands
        public RelayCommand CreateNewUserCommand => new RelayCommand(execute => CreateNewUser());
        public RelayCommand CancelWindowCommand => new RelayCommand(execute => CancelWindow()); //COmmand för att avbryta att stänga fönstret.

        //ObservableCollections
        private ObservableCollection<string> countries;

        public ObservableCollection<string> Countries
        {
            get { return countries; }
            set
            {
                countries = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> securityQuestion;

        public ObservableCollection<string> SecurityQuestion
        {
            get { return securityQuestion; }
            set
            {
                securityQuestion = value;
                OnPropertyChanged();
            }
        }
        //Egenskaper
        private User newUser;
        public User NewUser
        {
            get { return newUser; }
            set
            {
                newUser = value;
                OnPropertyChanged();
            }
        }
        private string inputUsername;

        public string InputUsername
        {
            get { return inputUsername; }
            set
            {
                inputUsername = value;
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

        private string selectedCountry;

        public string SelectedCountry
        {
            get { return selectedCountry; }
            set
            {
                selectedCountry = value;
                OnPropertyChanged();
            }
        }

        private double sliderValue;

        public double SliderValue
        {
            get { return sliderValue; }
            set
            {
                sliderValue = value;
                OnPropertyChanged();
            }
        }

        private string selectedSecurityQuestion;

        public string SelectedSecurityQuestion
        {
            get { return selectedSecurityQuestion; }
            set
            {
                selectedSecurityQuestion = value;
                OnPropertyChanged();
            }
        }

        private string securityQuestionAnswer;

        public string SecurityQuestionAnswer
        {
            get { return securityQuestionAnswer; }
            set
            {
                securityQuestionAnswer = value;
                OnPropertyChanged();
            }
        }


        //Konstruktor
        public RegisterWindowViewModel()

        {
            Countries = new ObservableCollection<string>() //Lista med Länder
			{
            "Afghanistan", "Albania", "Algeria", "Andorra",
            "Angola", "Antigua & Deps", "Argentina", "Armenia",
            "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain",
            "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize",
            "Benin", "Bhutan", "Bolivia", "Bosnia Herzegovina", "Botswana",
            "Brazil", "Brunei", "Bulgaria", "Burkina", "Burundi", "Cambodia",
            "Cameroon", "Canada", "Cape Verde", "Central African Rep", "Chad",
            "Chile", "China", "Colombia", "Comoros", "Congo", "Congo Democratic Rep",
            "Costa Rica", "Croatia", "Cuba", "Cyprus", "Czech Republic", "Denmark",
            "Djibouti", "Dominica", "Dominican Republic", "East Timor", "Ecuador", "Egypt",
            "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Fiji", "Finland",
            "France", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Greece", "Grenada", "Guatemala", "Guinea",
            "Guinea-Bissau", "Guyana", "Haiti", "Honduras", "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq",
            "Ireland Republic", "Israel", "Italy", "Ivory Coast", "Jamaica", "Japan", "Jordan",
            "Kazakhstan", "Kenya", "Kiribati", "Korea North", "Korea South", "Kosovo", "Kuwait",
            "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya",
            "Liechtenstein", "Lithuania", "Luxembourg", "Macedonia", "Madagascar", "Malawi",
            "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Mauritania", "Mauritius",
            "Mexico", "Micronesia", "Moldova", "Monaco", "Mongolia", "Montenegro", "Morocco", "Mozambique",
            "Myanmar, Burma", "Namibia", "Nauru", "Nepal", "Netherlands", "New Zealand", "Nicaragua", "Niger",
            "Nigeria", "Norway", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru",
            "Philippines", "Poland", "Portugal", "Qatar", "Romania", "Russian Federation", "Rwanda",
            "St Kitts & Nevis", "St Lucia", "Saint Vincent & the Grenadines", "Samoa", "San Marino",
            "Sao Tome & Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone",
            "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Sudan",
            "Spain", "Sri Lanka", "Sudan", "Suriname", "Swaziland", "Sweden", "Switzerland", "Syria", "Taiwan",
            "Tajikistan", "Tanzania", "Thailand", "Togo", "Tonga", "Trinidad & Tobago", "Tunisia", "Turkey",
            "Turkmenistan", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates",
            "United Kingdom", "United States", "Uruguay", "Uzbekistan", "Vanuatu",
            "Vatican City", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabwe"
            };

            SecurityQuestion = new ObservableCollection<string>() //Lista med Säkerhetsfrågor
            {
                "Vad heter/hette ditt första husdjur?",
                "Vilket märke hade din första bil?",
                "Vem är din favorit författare?"

            };



        }
        private void CancelWindow() //Metod som frågar användaren om de är säkra på att de vill avbryta och om de väljer Yes, så stängs fönstret och MainWindow öppnas.
        {
            MessageBoxResult result = MessageBox.Show("Är du säker på att du vill avrbyta?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow(); //Skapar den nya SplashScreenen
                mainWindow.Show();

                foreach (Window window in Application.Current.Windows)  //går igenom öppna fönster
                {
                    if (window is RegisterWindow) // om ett fönster som är öppet heter RegisterWindow. Stäng det
                    {
                        window.Close();
                        break;
                    }
                }

            }


        }
        private void CreateNewUser()
        {
            bool isPasswordValid = InputPassword.Length >= 8 &&  //Variabel för att kontrollera om Passwordet är gilitigt
                   InputPassword.Any(char.IsDigit) &&
                   InputPassword.Any(ch => "!@#$%^&*()_+-={}[]:;\"'<>,.?/\\|~`".Contains(ch)) && 
                   InputPassword.Any(char.IsLetter);

            if (string.IsNullOrWhiteSpace(InputUsername) || //Kontrollerar så att alla fält är ifyllda
                string.IsNullOrWhiteSpace(InputPassword) ||
                string.IsNullOrWhiteSpace(inputConfirmPassword) ||
                string.IsNullOrWhiteSpace(SelectedCountry) ||
                string.IsNullOrWhiteSpace(SelectedSecurityQuestion) ||
                string.IsNullOrWhiteSpace(SecurityQuestionAnswer) ||
                SliderValue <= 0) 
            
                {
                    MessageBox.Show("Alla fält är inte ifylda korrekt");
                    return;
                }
                if (InputUsername.Length < 3 || InputUsername.Length > 15)  //Kontrollerar att användarnamnet har rätt längd
                {
                    MessageBox.Show("Användarnamnet ska vara minst 3 bokstäver och max 15 bokstäver");
                    return;
                }
                if (!isPasswordValid) // Kontrulerar att lösenordet innehåller specialtecken
                {
                    MessageBox.Show("Lösenordet måste innehålla minst 8 tecken varav minst en(1) bokstav, en(1) siffra och ett(1) specialtecken");
                    return;
                }

            foreach (User user in UserManager.Users)
            {

                if (user.Username == InputUsername) //Kollar om användarnamnet inte är upptaget
                {
                    MessageBox.Show("Usernamet är upptaget"); //Felmeddelande om användarnamnet är upptaget
                    return;
                }
                
            }

            if (InputPassword == inputConfirmPassword) //Kontrollerar om lösenordet är samma
            {

                NewUser = new User($"{InputUsername}", $"{InputPassword}", $"{SelectedCountry}", $"{SelectedSecurityQuestion}", $"{SecurityQuestionAnswer}", SliderValue, UserManager.Users.Count + 1); //skapar ny användare
                User.AddUser(newUser); //Lägger till ny användare
                MessageBox.Show("Ny användare har skapats", "Välkommnen!");
                MainWindow mainWindow = new MainWindow(); //Skapar den nya SplashScreenen
                mainWindow.Show();

                foreach (Window window in Application.Current.Windows)  //går igenom öppna fönster
                {
                    if (window is RegisterWindow) // om ett fönster som är öppet heter RegisterWindow. Stäng det
                    {
                        window.Close();
                        break;
                    }
                }



            }
            else
            {
                MessageBox.Show("Passworden matchar inte");
            }
        }
                

            

    }
}

