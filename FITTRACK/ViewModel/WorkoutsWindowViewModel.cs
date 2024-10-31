using FITTRACK.Model;
using FITTRACK.MVVM;
using FITTRACK.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using static FITTRACK.ViewModel.WorkoutsWindowViewModel;

namespace FITTRACK.ViewModel
{
    class WorkoutsWindowViewModel : ViewModelBase
    {   //UserManager
        public User CurrentUser => UserManager.Instance.CurrentUser;

        //Commands
        public RelayCommand FilteredWorkoutsCommand => new RelayCommand(execute => ExecuteFilterWorkouts());
        public RelayCommand OpenWorkoutCommand => new RelayCommand(execute => OpenWorkout(), canExecute => selectedWorkout != null);
        public RelayCommand RemoveWorkoutCommand => new RelayCommand(execute => RemoveWorkout(), canExecute => selectedWorkout != null);
        public RelayCommand AddWorkoutWindowCommand => new RelayCommand(execute => AddWorkoutWindow());
        public RelayCommand InfoBoxCommand => new RelayCommand(execute => InfoBox());
        public RelayCommand OpenUserDetailsWindowCommand => new RelayCommand(execute => OpenUserDetailsWindow());
        public RelayCommand LogOutToMainCommand => new RelayCommand(execute => LogOutToMain());


        //ObservableCollection
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
       
        private ObservableCollection<string> dateFilterOptions;

        public ObservableCollection<string> DateFilterOptions
        {
            get { return dateFilterOptions; }
            set
            {
                dateFilterOptions = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> durationFilterOptions;

        public ObservableCollection<string> DurationFilterOptions
        {
            get { return durationFilterOptions; }
            set
            {
                durationFilterOptions = value;
                OnPropertyChanged();
            }
        }
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
        public ICollectionView WorkoutView { get; set; } // Vy för filtrerade träningspass



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
        // Egenskaper för filterkriterier
        private string selectedDateFilterOptions;
        public string SelectedDateFilterOptions
        {
            get { return selectedDateFilterOptions; }
            set
            {
                selectedDateFilterOptions = value;
                OnPropertyChanged(); 
            }
        }

        private string filterType;
        public string FilterType
        {
            get { return filterType; }
            set
            {
                filterType = value;
                OnPropertyChanged(); 
            }
        }

        private string selectedDurationFilterOption;
        public string SelectedDurationFilterOption
        {
            get { return selectedDurationFilterOption; }
            set
            {
                selectedDurationFilterOption = value;
                OnPropertyChanged(nameof(SelectedDurationFilterOption));
                
            }
        }

        //Konstruktor
        public WorkoutsWindowViewModel() 
        {
            WorkoutType = new ObservableCollection<string>() //sätter värden till typ
            {
                "Alla","Strength", "Cardio"
            };
            DateFilterOptions = new ObservableCollection<string> //sätter värden till datum
        {
            "Alla",
            "Idag",
            "Senaste Veckan",
            "Senaste Månaden"
        };
            DurationFilterOptions = new ObservableCollection<string> //Sätter värden till duration
        {
            "All",
            "<20 min",
            "20-39 min",
            "40-59 min",
            "60-79 min",
            "80-99 min",
            "100-119 min",
            ">120 min"
        };
            var currentUser = UserManager.Instance.CurrentUser;

            if (currentUser is AdminUser adminUser)
            {
                // Om användaren är en AdminUser, sätt alla användares workouts
                Workouts = adminUser.GetAllWorkouts();
            }
            else
            {
                // Om användaren är en vanlig User, sätt den inloggade användarens workouts
                Workouts = CurrentUser.Workouts; //Läger till träningarna i Workoutslistan
            }
                
          WorkoutView = CollectionViewSource.GetDefaultView(Workouts); // Skapa vy för filtrering
          WorkoutView.Filter = FilterWorkouts; // Sätt filtermetod
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
            MessageBox.Show("FitTrack startades 2024.\n\nVi erbjuder en App för att hantera dina träningar på ett enkelt och smidigt sätt.\nPå denna sida kan du lägga till, ta bort, få information om ditt pass,\nredigera användare och logga ut.","FitTrack Info", MessageBoxButton.OK);
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
                var currentUser = UserManager.Instance.CurrentUser;

                if (currentUser is AdminUser)
                {
                    // Hitta användaren som äger träningspasset och ta bort det från deras lista
                    foreach (var user in UserManager.Users)
                    {
                        if (user.Workouts.Contains(SelectedWorkout))
                        {
                            user.RemoveWorkout(SelectedWorkout);
                            break;
                        }
                    }
                }
                else
                {
                    // Ta bort träningspasset från den inloggade användarens lista
                    currentUser.RemoveWorkout(SelectedWorkout);
                }

                

                    Workouts.Remove(SelectedWorkout); // tar bort workouten

                
                // Uppdatera Workouts-listan i ViewModel
                RefreshWorkouts();
            }
        }

        private void RefreshWorkouts() //Updaterar listan
        {
            var currentUser = UserManager.Instance.CurrentUser;

            if (currentUser is AdminUser adminUser)
            {
                Workouts = adminUser.GetAllWorkouts();
            }
            else
            {
                Workouts = currentUser.Workouts;
            }
        }
        





    public void OpenWorkout()  // öppnar valda träningen i WorkoutDetails window
        {
           
            WorkoutsWindowViewModel workoutsWindowViewModel = new WorkoutsWindowViewModel();
            WorkoutDetailsWindow workoutDetailsWindow = new WorkoutDetailsWindow(SelectedWorkout);
            workoutDetailsWindow.Show();

        }

        private void ExecuteFilterWorkouts()
        {
            WorkoutView.Refresh(); // Uppdatera vyn för att tillämpa filter
        }

        // Metod för att filtrera träningspass baserat på kriterier
        private bool FilterWorkouts(object item)
        {
            var workout = item as Workout;
            if (workout == null) return false;

            bool matchesDate = SelectedDateFilterOptions switch  //Bool för att kolla vilket värde som är valt
            {
                "Idag" => workout.Date.Date == DateTime.Today,
                "Senaste Veckan" => workout.Date.Date >= DateTime.Today.AddDays(-7),
                "Senaste Månaden" => workout.Date.Date >= DateTime.Today.AddDays(-31),
                _ => true // Om "Alla" är valt, matcha alla datum
            };

            bool matchesType = FilterType switch // Bool för att kolla vilket värde som är valt
            {
                "Strength" => workout.Type.Equals("Strength", StringComparison.OrdinalIgnoreCase),
                "Cardio" => workout.Type.Equals("Cardio", StringComparison.OrdinalIgnoreCase),
                _ => true // Om "Alla" är valt, matcha alla typer
            };
            bool matchesDuration = SelectedDurationFilterOption switch
            {
                "<20 min" => workout.Duration.TotalMinutes < 20,
                "20-39 min" => workout.Duration.TotalMinutes >= 20 && workout.Duration.TotalMinutes < 40,
                "40-59 min" => workout.Duration.TotalMinutes >= 40 && workout.Duration.TotalMinutes < 60,
                "60-79 min" => workout.Duration.TotalMinutes >= 60 && workout.Duration.TotalMinutes < 80,
                "80-99 min" => workout.Duration.TotalMinutes >= 80 && workout.Duration.TotalMinutes < 100,
                "100-119 min" => workout.Duration.TotalMinutes >= 100 && workout.Duration.TotalMinutes < 120,
                ">120 min" => workout.Duration.TotalMinutes >= 120,
                _ => true // Om "Alla" är valt, matcha alla varaktigheter
            };

            return matchesDate && matchesType && matchesDuration; // Returnera true om alla kriterier matchar
        }
        
    }
}
