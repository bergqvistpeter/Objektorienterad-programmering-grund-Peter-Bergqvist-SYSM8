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
        private User user;

        //Konstruktor
        public StrengthWorkout(DateTime date, string type, TimeSpan duration, double CaloriesBurned, string notes, User user) : base(date, type, duration, CaloriesBurned, notes)
        {
            this.user = user;
        }

        //Metod
        public override double CalculateCaloriesBurnd() //Metod som räknar ut kcal som man bränner
        {
            CaloriesBurned = (Duration.TotalMinutes * StaticWorkoutValue * METStrength * user.GetUserWeight()) / 200;
            return CaloriesBurned;
        }
    }
}
