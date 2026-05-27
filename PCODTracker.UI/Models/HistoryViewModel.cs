namespace PCODTracker.UI.Models
{
    public class HistoryViewModel
    {
        public int WaterIntake { get; set; }

        public bool ExerciseDone { get; set; }

        public int SleepHours { get; set; }

        public bool HealthyFood { get; set; }

        public string Mood { get; set; } = string.Empty;
    }
}