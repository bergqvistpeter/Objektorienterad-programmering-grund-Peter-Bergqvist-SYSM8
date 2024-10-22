using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITTRACK.Model
{
    public abstract class Workout
    {
        //Egenskaper
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public double CaloriesBurned { get; set; }
        public string Notes { get; set; }
        //Konstruktor
        public Workout(DateTime date, string type, TimeSpan duration, double CaloriesBurned, string notes) 
        { 
            this.Date = date;
            this.Type = type;
            this.Duration = duration;
            this.CaloriesBurned = CaloriesBurned;
            this.Notes = notes;
        }
        //Metod
        public abstract double CalculateCaloriesBurnd(); 
        
    }
}
