using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITTRACK.Model
{
    public class StrengthWorkout : Workout
    {   //Egenskaper

        private double METStrength = 5;
        private double StaticWorkoutValue = 3.5;
        private int Weight = 70;

        //Konstruktor
        //public StrengthWorkout(DateTime date, string type, TimeSpan duration, double CaloriesBurned, string notes) : base(date, type, duration, CaloriesBurned, notes)
        //{
            
        //}

        //Metod
        public override double CalculateCaloriesBurnd(double weight) //Metod som räknar ut kcal som man bränner
        {
            CaloriesBurned = (Duration.TotalMinutes * StaticWorkoutValue * METStrength * weight) / 200;
            return CaloriesBurned;
        }
    }
}
