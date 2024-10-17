using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITTRACK.Model
{
    public abstract class Workout
    {
        public DateTime Date;
        public string Type;
        public TimeSpan Duration;
        public double CaloriesBurned;
        public string Notes;

        public Workout(DateTime date, string type, TimeSpan duration, double CaloriesBurned, string notes) 
        { 
            this.Date = date;
            this.Type = type;
            this.Duration = duration;
            this.CaloriesBurned = CaloriesBurned;
            this.Notes = notes;
        }
    }
}
