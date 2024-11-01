using FITTRACK.Model;
using FITTRACK.MVVM;
using FITTRACK.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FITTRACK.ViewModel
{
    class UserDetailsWindowViewModel : ViewModelBase
    {
        public User CurrentUser => UserManager.Instance.CurrentUser; //CurrentUser
        //Command
        public RelayCommand CancelWindowCommand => new RelayCommand(execute => CancelWindow()); //Command för att avbryta att stänga fönstret.

        public RelayCommand UpdateUserCommand => new RelayCommand(execute => UpdateUser()); //Command för att avbryta att stänga fönstret.
        
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
        private string tempUsername;
        private string tempPassword;
        private string tempCountry;
        private double tempWeight;
        private string tempSecurityQuestion;
        private string tempSecurityAnswer;

        //Konstruktor
        public UserDetailsWindowViewModel() 
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
            tempUsername = CurrentUser.Username;
            tempPassword = CurrentUser.Password;
            tempCountry = CurrentUser.Country;
            tempWeight = CurrentUser.Weight;
            tempSecurityQuestion = CurrentUser.SecurityQuestion;
            tempSecurityAnswer = CurrentUser.SecurityAnswer;
        
        }


        //Metod
        private void CancelWindow() //Metod som frågar användaren om de är säkra på att de vill avbryta och om de väljer Yes, så stängs fönstret och MainWindow öppnas.
        {
            MessageBoxResult result = MessageBox.Show("Är du säker på att du vill avrbyta?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                CurrentUser.Username = tempUsername;
                CurrentUser.Password = tempPassword;
                CurrentUser.Country = tempCountry;
                CurrentUser.Weight = tempWeight;
                CurrentUser.SecurityQuestion = tempSecurityQuestion;
                CurrentUser.SecurityAnswer = tempSecurityAnswer;

                foreach (Window window in Application.Current.Windows)  //går igenom öppna fönster
                {
                    if (window is UserDetailsWindow) // om ett fönster som är öppet heter RegisterWindow. Stäng det
                    {
                        window.Close();
                        break;
                    }
                }

            }
        }

        private void UpdateUser()
        {
            bool isPasswordValid = CurrentUser.Password.Length >= 8 &&  //Variabel för att kontrollera om Passwordet är gilitigt
                   CurrentUser.Password.Any(char.IsDigit) &&
                   CurrentUser.Password.Any(ch => "!@#$%^&*()_+-={}[]:;\"'<>,.?/\\|~`".Contains(ch)) &&
                   CurrentUser.Password.Any(char.IsLetter);

            if (string.IsNullOrWhiteSpace(CurrentUser.Username) || //Kontrollerar så att alla fält är ifyllda
                string.IsNullOrWhiteSpace(CurrentUser.Password) ||
                string.IsNullOrWhiteSpace(InputConfirmPassword) ||
                string.IsNullOrWhiteSpace(CurrentUser.Country) ||
                string.IsNullOrWhiteSpace(CurrentUser.SecurityQuestion) ||
                string.IsNullOrWhiteSpace(CurrentUser.SecurityAnswer) ||
                CurrentUser.Weight <= 0)

            {
                MessageBox.Show("Alla fält är inte ifylda korrekt");
                return;
            }
            if (CurrentUser.Username.Length < 3 || CurrentUser.Username.Length > 15)  //Kontrollerar att användarnamnet har rätt längd
            {
                MessageBox.Show("Användarnamnet ska vara minst 3 bokstäver och max 15 bokstäver");
                return;
            }
            if (!isPasswordValid) // Kontrulerar att lösenordet innehåller specialtecken
            {
                MessageBox.Show("Lösenordet måste innehålla minst 8 tecken, 1 siffra och 1 specialtecken");
                return;
            }

            foreach (User user in UserManager.Users)
            {

                if (user.Username == CurrentUser.Username && user.UserID != CurrentUser.UserID) //Kollar om användarnamnet inte är upptaget och att det inte är samma userID
                {
                    MessageBox.Show("Usernamet är upptaget"); //Felmeddelande om användarnamnet är upptaget
                    return;
                }

            }

            if (CurrentUser.Password == inputConfirmPassword) //Kontrollerar om lösenordet är samma
            {
                
                
               
               UpdateCurrentUser();



            }
            else
            {
                MessageBox.Show("Passworden matchar inte");
            }
        }

        public void UpdateCurrentUser() 
        { //Då TwoWay Mode används ska det updateras automatiskt
            MessageBox.Show("Användaren har uppdaterats", "Update!");

            foreach (Window window in Application.Current.Windows)  //går igenom öppna fönster
            {
                if (window is UserDetailsWindow) // om ett fönster som är öppet heter RegisterWindow. Stäng det
                {
                    window.Close();
                    break;
                }
            }
        }


    }

}

