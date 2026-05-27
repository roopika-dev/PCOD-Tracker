namespace PCODTracker.API.DTOs
{
    public class DailyHealthDto
    {
        public string UserId { get; set; } = "";

        public string Date { get; set; } = "";

        public int WaterIntake { get; set; }

        public bool ExerciseDone { get; set; }

        public int SleepHours { get; set; }

        public bool HealthyFood { get; set; }

        public string Mood { get; set; } = "";
    }
}