using FITTRACK.Model;
using FITTRACK.MVVM;
using FITTRACK.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FITTRACK.ViewModel
{
    class WorkoutsWindowViewModel
    {   //Commands
        public RelayCommand AddWorkoutWindowCommand => new RelayCommand(execute => AddWorkoutWindow());
        public RelayCommand InfoBoxCommand => new RelayCommand(execute => InfoBox());
        public RelayCommand OpenUserDetailsWindowCommand => new RelayCommand(execute => OpenUserDetailsWindow());
        public RelayCommand LogOutToMainCommand => new RelayCommand(execute => LogOutToMain());
        //Egenskaper
        private UserManager userManager;

        //Konstruktor
        public WorkoutsWindowViewModel() 
        {
            userManager = new UserManager();
        }
        //Metod
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
    }
}
