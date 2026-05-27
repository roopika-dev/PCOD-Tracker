using Google.Cloud.Firestore;

namespace PCODTracker.API.Models
{
    [FirestoreData]
    public class DailyHealth
    {
        [FirestoreProperty]
        public string Id { get; set; }

        [FirestoreProperty]
        public string UserId { get; set; }

        [FirestoreProperty]
        public DateTime Date { get; set; }

        [FirestoreProperty]
        public int WaterIntake { get; set; }

        [FirestoreProperty]
        public bool ExerciseDone { get; set; }

        [FirestoreProperty]
        public int SleepHours { get; set; }

        [FirestoreProperty]
        public bool HealthyFood { get; set; }

        [FirestoreProperty]
        public string Mood { get; set; }
    }
}