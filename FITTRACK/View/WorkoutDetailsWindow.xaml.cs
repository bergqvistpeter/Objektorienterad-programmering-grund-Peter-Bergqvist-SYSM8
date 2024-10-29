using FITTRACK.Model;
using FITTRACK.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FITTRACK.View
{
    /// <summary>
    /// Interaction logic for WorkoutDetailsWindow.xaml
    /// </summary>
    public partial class WorkoutDetailsWindow : Window
    {
        public WorkoutDetailsWindow(Workout SelectedWorkout)
        {
            InitializeComponent();
            WorkoutDetailsWindowViewModel viewModel = new WorkoutDetailsWindowViewModel(SelectedWorkout); //Skapat ViewModel och bundit den.
            DataContext = viewModel;
        }
    }
}

