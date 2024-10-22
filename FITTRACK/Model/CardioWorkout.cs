using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FITTRACK.Model
{
    public class CardioWorkout : Workout
    {
        //Egenskaper

        private double METRunning = 8;
        private double StaticWorkoutValue = 3.5;
        private User user;

        //Konstruktor
        public CardioWorkout(DateTime date, string type, TimeSpan duration, double CaloriesBurned, string notes) : base(date, type, duration, CaloriesBurned, notes)
        {
            
        }


        public override double CalculateCaloriesBurnd()
        { 
            CaloriesBurned = (Duration.TotalMinutes * StaticWorkoutValue * METRunning * user.GetUserWeight())/ 200;
            return CaloriesBurned;
        }
    }
}
