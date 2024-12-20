﻿using System;
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
        

        

        //Metod 
        public override double CalculateCaloriesBurnd(double weight) // Metod som räknar ut Kalorier
        { 
            CaloriesBurned = (Duration.TotalMinutes * StaticWorkoutValue * METRunning * weight )/ 200;
            return CaloriesBurned;
        }
    }
}
