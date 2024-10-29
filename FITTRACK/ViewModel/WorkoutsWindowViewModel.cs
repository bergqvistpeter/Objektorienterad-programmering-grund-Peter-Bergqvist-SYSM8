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
        public RelayCommand OpenWorkoutCommand => new RelayCommand(execute => OpenWorkout(), canExecute => selectedWorkout != null);
        public RelayCommand RemoveWorkoutCommand => new RelayCommand(execute => RemoveWorkout(), canExecute => selectedWorkout != null);
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
        private Workout selectedWorkout;

        public Workout SelectedWorkout
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
           
        

            Workouts = CurrentUser.Workouts; //Läger till träningarna i Workoutslistan
        }




        //Metod
        
        private void OpenUserDetailsWindow() // Metod som öppnar redigera konto fönster och stänger det gamla
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow(); //Skapar det nya userdetails fönstret
            userDetailsWindow.Show(); //Öppnar det nya userdetails fönstret
            
           
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
                                                 
        }
        public void RemoveWorkout()
        {
            
                MessageBoxResult result = MessageBox.Show("Är du säker på att du vill ta bort det valda passet?", "Ta bort", MessageBoxButton.YesNo, MessageBoxImage.Warning); //Fråga om du är säker på att du vill ta bort ditt pass
                if (result == MessageBoxResult.Yes)

                {
                    Workouts.Remove(SelectedWorkout); // tar bort workouten
                }

               
            
        }

        public void OpenWorkout() 
        {
           
            WorkoutsWindowViewModel workoutsWindowViewModel = new WorkoutsWindowViewModel();
            WorkoutDetailsWindow workoutDetailsWindow = new WorkoutDetailsWindow(SelectedWorkout);
            workoutDetailsWindow.Show();

        }

    }
}
