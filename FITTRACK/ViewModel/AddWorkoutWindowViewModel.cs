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
using System.Windows.Input;
using System.Windows.Media.Media3D;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FITTRACK.ViewModel
{
    public class AddWorkoutWindowViewModel : ViewModelBase
    {   //UserManager
        public User CurrentUser => UserManager.Instance.CurrentUser; //CurrentUser
        //Command
        public RelayCommand SaveWorkoutCommand => new RelayCommand(execute => SaveWorkout());
        public RelayCommand CancelWindowCommand => new RelayCommand(execute => CancelWindow()); //Command för att avbryta att stänga fönstret.
        //ObservableCollections
        private ObservableCollection<Workout> workouts;
        public ObservableCollection<Workout> Workouts
        {
            get { return workouts; }
            set
            {
                workouts = value;
                OnPropertyChanged();
            }
        }

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

        private void SaveWorkout() 
        {
            
            if (string.IsNullOrWhiteSpace(InputWorkoutType) || string.IsNullOrWhiteSpace(InputNotes) || InputDate == DateTime.MinValue || InputDuration == TimeSpan.Zero)  //Kontrollerar så att alla fält är ifyllda
            {
                MessageBox.Show("Alla uppgifter är inte korrekt ifyllda"); // om inte allt är ifyllt korrekt visas denna
                return;
            }

            if (InputWorkoutType == "Strength") //Om strength är vald, då skapar man en ny strength workout
            {


                Workout workout = new StrengthWorkout { Date = InputDate, Type = InputWorkoutType, Duration = InputDuration, Notes = InputNotes }; //Skapar ny workout


                double tempCaloriesBurned = workout.CalculateCaloriesBurnd(GetWeight()); //använder metoden från strength klassen för att räkna ut CaloriesBurnt
                workout.CaloriesBurned = tempCaloriesBurned; //Sätter värdet till CaloriesBurned
                CurrentUser.Workouts.Add(workout); // Läger till det i User Workouts
                Workouts = CurrentUser.Workouts; // updaterar workoutlistan

            }
            else //Om inte strength är ifylld, då är det ett Cardio pass
            {
                Workout workout = new CardioWorkout { Date = inputDate, Type = InputWorkoutType, Duration = InputDuration, Notes = InputNotes }; //SKapar ny workout


                double tempCaloriesBurned = workout.CalculateCaloriesBurnd(GetWeight()); // Använder metoden för att räkna ut Calories Burnet från Cardioklassen
                workout.CaloriesBurned = tempCaloriesBurned; // Sätter CaloriesBurned till workouten
                CurrentUser.Workouts.Add(workout); // lägger till workouten i User Workouts
                Workouts = CurrentUser.Workouts; // lägger till i Workoutslistan
            }

            foreach (Window window in Application.Current.Windows)  //går igenom öppna fönster
            {
                if (window is AddWorkoutWindow) // om ett fönster som är öppet heter RegisterWindow. Stäng det
                {
                    window.Close();
                    break;
                }
            }


        }

        private double GetWeight()
        {
            return CurrentUser.Weight;
        }
        private void CancelWindow() //Metod som frågar användaren om de är säkra på att de vill avbryta och om de väljer Yes, så stängs fönstret och MainWindow öppnas.
        {
            MessageBoxResult result = MessageBox.Show("Är du säker på att du vill avrbyta?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
              

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
