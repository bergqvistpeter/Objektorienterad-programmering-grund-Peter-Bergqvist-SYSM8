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
    class WorkoutsWindowViewModel : ViewModelBase
    {
        public User CurrentUser => UserManager.Instance.CurrentUser;
        //Commands
        public RelayCommand AddWorkoutWindowCommand => new RelayCommand(execute => AddWorkoutWindow());
        public RelayCommand InfoBoxCommand => new RelayCommand(execute => InfoBox());
        public RelayCommand OpenUserDetailsWindowCommand => new RelayCommand(execute => OpenUserDetailsWindow());
        public RelayCommand LogOutToMainCommand => new RelayCommand(execute => LogOutToMain());


        //ObservableCollection
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


        //Egenskaper
        private ObservableCollection<Workout> selectedWorkout;

        public ObservableCollection<Workout> SelectedWorkout
        {
            get { return selectedWorkout; }
            set 
            { 
                selectedWorkout = value;
                OnPropertyChanged();
            }
        }


        //Konstruktor
        public WorkoutsWindowViewModel() 
        {
            Workouts = new ObservableCollection<Workout>();

            CurrentUser.Workouts.Add(workout1);
            CurrentUser.Workouts.Add(workout2);
        }
        //Metod

        public void AddWorkout(Workout workout) 
        { 
            Workouts.Add(workout);
        }
        private void OpenUserDetailsWindow() // Metod som öppnar redigera konto fönster och stänger det gamla
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow(); //Skapar det nya userdetails fönstret
            userDetailsWindow.Show(); //Öppnar det nya userdetails fönstret
            
            foreach (Window window in Application.Current.Windows)  //går igenom öppna fönster
            {
                if (window is WorkoutsWindow) // om ett fönster som är öppet heter WorkoutsWindow. Stäng det
                {
                    window.Close();
                    break;
                }
            }
        }
        private void LogOutToMain() 
        
        
        {
                MessageBoxResult result = MessageBox.Show("Är du säker på att du vill logga ut?", "Logga ut", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    MainWindow mainWindow = new MainWindow(); //Skapar den nya SplashScreenen
                    mainWindow.Show();

                    foreach (Window window in Application.Current.Windows)  //går igenom öppna fönster
                    {
                        if (window is WorkoutsWindow) // om ett fönster som är öppet heter RegisterWindow. Stäng det
                        {
                            window.Close();
                            break;
                        }
                    }

                }


        }
        private void InfoBox() //InfoBox som öppnar ett fönster med info
        {
            MessageBox.Show("FitTrack startades 2024.\n\nVi erbjuder en App för att hantera dina träningar på ett enkelt och smidigt sätt.","FitTrack Info", MessageBoxButton.OK);
        }
        
        private void AddWorkoutWindow()
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow(); //Skapar det nya Workout fönstret
            addWorkoutWindow.Show(); //Öppnar det nya workoutfönstret
                                     
            
                

                foreach (Window window in Application.Current.Windows)  //går igenom öppna fönster
                {
                    if (window is WorkoutsWindow) // om ett fönster som är öppet heter WorkoutsWindow. Stäng det
                    {
                        window.Close();
                        break;
                    }
                }

            
        }
        Workout workout1 = new StrengthWorkout
        {
            Date = DateTime.Now,
            Type = "Cardio",
            Duration = new TimeSpan(0, 30, 0),
            CaloriesBurned = 300,
            Notes = "Löpbandet"
        };

        Workout workout2 = new CardioWorkout
        {
            Date = DateTime.Now,
            Type = "Strength",
            Duration = new TimeSpan(1, 0, 0),
            CaloriesBurned = 300,
            Notes = "Överkropp"
        };
        
    }
}
