using FITTRACK.Model;
using FITTRACK.MVVM;
using FITTRACK.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FITTRACK.ViewModel
{
    public class AddWorkoutWindowViewModel : ViewModelBase
    {
        public User CurrentUser => UserManager.Instance.CurrentUser; //CurrentUser
        //Command
        public RelayCommand CancelWindowCommand => new RelayCommand(execute => CancelWindow()); //Command för att avbryta att stänga fönstret.


       
        private ObservableCollection<string> workoutType;

        public ObservableCollection<string> WorkoutType
        {
            get { return workoutType; }
            set
            {
                workoutType = value;
                OnPropertyChanged();
            }
        }
        //Egenskaper
        private DateTime inputDate;

        public DateTime InputDate
        {
            get { return inputDate; }
            set 
            {
                inputDate = value; 
                OnPropertyChanged();
            }
        }
        private string inputWorkoutType;

        public string InputWorkoutType
        {
            get { return inputWorkoutType; }
            set 
            { 
                inputWorkoutType = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan inputDuration;

        public TimeSpan InputDuration
        {
            get { return inputDuration; }
            set 
            { 
                inputDuration = value;
                OnPropertyChanged();
            }
        }

        private string inputNotes;

        public string InputNotes
        {
            get { return inputNotes; }
            set 
            { 
                inputNotes = value;
                OnPropertyChanged();
            }
        }

        


        //Konstruktor
        public AddWorkoutWindowViewModel()
        {
            WorkoutType = new ObservableCollection<string>()
            {
                "Strength", "Cardio"
            };
            
            inputDate = DateTime.Now; // Sätter dagens datum som default till träningspasset som ska läggas till

            
        }
        private void CancelWindow() //Metod som frågar användaren om de är säkra på att de vill avbryta och om de väljer Yes, så stängs fönstret och MainWindow öppnas.
        {
            MessageBoxResult result = MessageBox.Show("Är du säker på att du vill avrbyta?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                WorkoutsWindow workoutsWindow = new WorkoutsWindow(); //Skapar den nya SplashScreenen
                workoutsWindow.Show();

                foreach (Window window in Application.Current.Windows)  //går igenom öppna fönster
                {
                    if (window is AddWorkoutWindow) // om ett fönster som är öppet heter RegisterWindow. Stäng det
                    {
                        window.Close();
                        break;
                    }
                }

            }
        }

    }
}
