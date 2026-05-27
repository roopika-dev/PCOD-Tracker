using System.ComponentModel.DataAnnotations;

namespace PCODTracker.UI.Models
{
    public class DailyHealthViewModel
    {
        public string UserId { get; set; } = "";

        public int WaterIntake { get; set; }

        public bool ExerciseDone { get; set; }
        [Range(1, 24)]
        public int SleepHours { get; set; }

        public bool HealthyFood { get; set; }

        [Required]
        public string Mood { get; set; } = "";
    }
}