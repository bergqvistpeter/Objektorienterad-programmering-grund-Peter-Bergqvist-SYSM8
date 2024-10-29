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
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace FITTRACK.ViewModel
{
    class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        public User CurrentUser => UserManager.Instance.CurrentUser; //UserManager
        //Commands
        public RelayCommand CopyToAddWorkoutWindowCommand => new RelayCommand(execute => CopyToAddWorkoutWindow()); //Command för att kopiera till AddWorkout, öppna det och att stänga fönstret.
        public RelayCommand CancelWindowCommand => new RelayCommand(execute => CancelWindow()); //Command för att avbryta att stänga fönstret.
        public RelayCommand SaveWorkoutCommand => new RelayCommand(execute => SaveWorkout(Workout)); //Command för att spara träning och att stänga fönstret.
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
        public Workout Workout { get; set; }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged();

            }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged();
                UpdateCaloriesBurned();


            }
        }

        private TimeSpan duration;

        public TimeSpan Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged();
                UpdateCaloriesBurned();
            }
        }

        private string notes;

        public string Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                OnPropertyChanged();

            }
        }
        private double caloriesBurned;
        public double CaloriesBurned
        {
            get { return caloriesBurned; }
            set
            {
                caloriesBurned = value;
                OnPropertyChanged();

            }
        }

        //Konstruktor

        public WorkoutDetailsWindowViewModel(Workout workout)
        {
            WorkoutType = new ObservableCollection<string>()
            {
                "Strength", "Cardio"
            };
            

            Date = workout.Date;
            Type = workout.Type;
            Duration = workout.Duration;
            Notes = workout.Notes;
            CaloriesBurned = workout.CaloriesBurned;

            Workout = workout;
        }
        //Method
        private double METStrength = 5;
        private double StaticWorkoutValue = 3.5;
        private double METRunning = 8;
        private void UpdateCaloriesBurned() //Updaterar calorierna efterhand
        {

            if (Type == "Cardio")
            {

                CaloriesBurned = (Duration.TotalMinutes * StaticWorkoutValue * METRunning * CurrentUser.Weight) / 200;
            }
            else if (Type == "Strength")
            {
                CaloriesBurned = (Duration.TotalMinutes * StaticWorkoutValue * METStrength * CurrentUser.Weight) / 200;
            }
        }
        private void CancelWindow() //Metod som frågar användaren om de är säkra på att de vill avbryta och om de väljer Yes, så stängs fönstret och MainWindow öppnas.
        {
            MessageBoxResult result = MessageBox.Show("Är du säker på att du vill avrbyta?", "Varning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {

                foreach (Window window in Application.Current.Windows)  //går igenom öppna fönster
                {
                    if (window is WorkoutDetailsWindow) // om ett fönster som är öppet heter WorkoutDetailsWindow. Stäng det
                    {
                        window.Close();
                        break;
                    }
                }

            }
        }
        public void SaveWorkout(Workout workout) //Sparar och updaterar workouten
        {
            CurrentUser.Workouts.Remove(workout); //Tar bort från listan
            workout.Date = Date;
            workout.Duration = Duration;
            workout.CaloriesBurned = CaloriesBurned;
            workout.Notes = Notes;
            workout.Type = Type;
            MessageBox.Show("Träningen har uppdaterats", "Update!");
            
            CurrentUser.Workouts.Add(workout); //Lägger till i listan
            foreach (Window window in Application.Current.Windows)  //går igenom öppna fönster
            {
                if (window is WorkoutDetailsWindow) // om ett fönster som är öppet heter RegisterWindow. Stäng det
                {
                    window.Close();
                    break;
                }
            }
        }

        private void CopyToAddWorkoutWindow() //Metod för att öppna en Kopia av Workouten.
        {
            if (Type == "Strength")
            {
                Workout copiedWorkout = new StrengthWorkout
                {
                    Date = this.Date,
                    Type = this.Type,
                    Duration = this.Duration,
                    Notes = this.Notes,
                    CaloriesBurned = this.CaloriesBurned
                };
                AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow
                {
                    DataContext = new WorkoutDetailsWindowViewModel(copiedWorkout)
                };
                addWorkoutWindow.Show();
            } else
            {
                Workout copiedWorkout = new CardioWorkout
                {
                    Date = this.Date,
                    Type = this.Type,
                    Duration = this.Duration,
                    Notes = this.Notes,
                    CaloriesBurned = this.CaloriesBurned
                };
                AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow() 
                {
                    DataContext = new AddWorkoutWindowViewModel(copiedWorkout)
                };
                addWorkoutWindow.Show();

            }
            foreach (Window window in Application.Current.Windows)  //går igenom öppna fönster
            {
                if (window is WorkoutDetailsWindow) // om ett fönster som är öppet heter WorkoutDetailWindow. Stäng det
                {
                    window.Close();
                    break;
                }
            }
        }

    }
}
