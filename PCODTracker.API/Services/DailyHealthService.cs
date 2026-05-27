using Google.Cloud.Firestore;
using PCODTracker.API.DTOs;

namespace PCODTracker.API.Services
{
    public class DailyHealthService
    {
        private readonly FirestoreDb _firestore;

        public DailyHealthService(FirestoreDb firestore)
        {
            _firestore = firestore;
        }

        // SAVE
        public async Task<string> AddAsync(
            string userId,
            DailyHealthDto dto)
        {
            var indiaTime =
                TimeZoneInfo.ConvertTimeBySystemTimeZoneId(
                    DateTime.UtcNow,
                    "India Standard Time");

            var todayDate =
                indiaTime.ToString("dd-MM-yyyy");

            var collection =
                _firestore
                .Collection("DailyHealth")
                .Document(userId.Trim())
                .Collection("Records");

            var snapshot =
                await collection.GetSnapshotAsync();

            DocumentSnapshot? existingDoc = null;

            // CHECK ALL RECORDS
            foreach (var doc in snapshot.Documents)
            {
                var data = doc.ToDictionary();

                if (data.ContainsKey("Date"))
                {
                    var existingDate =
                        data["Date"]?.ToString();

                    if (!string.IsNullOrEmpty(existingDate))
                    {
                        var existingOnlyDate =
                            existingDate.Split(' ')[0];

                        if (existingOnlyDate == todayDate)
                        {
                            existingDoc = doc;
                            break;
                        }
                    }
                }
            }

            // UPDATE EXISTING RECORD
            if (existingDoc != null)
            {
                await existingDoc.Reference.UpdateAsync(
                    new Dictionary<string, object>
                    {
                        { "Date", indiaTime.ToString("dd-MM-yyyy hh:mm tt") },
                        { "SortDate", Timestamp.FromDateTime(DateTime.UtcNow) },
                        { "WaterIntake", dto.WaterIntake },
                        { "ExerciseDone", dto.ExerciseDone },
                        { "SleepHours", dto.SleepHours },
                        { "HealthyFood", dto.HealthyFood },
                        { "Mood", dto.Mood }
                    });

                return "updated";
            }

            // CREATE NEW RECORD
            await collection.AddAsync(new
            {
                Date = indiaTime.ToString("dd-MM-yyyy hh:mm tt"),

                SortDate = Timestamp.FromDateTime(DateTime.UtcNow),

                WaterIntake = dto.WaterIntake,

                ExerciseDone = dto.ExerciseDone,

                SleepHours = dto.SleepHours,

                HealthyFood = dto.HealthyFood,

                Mood = dto.Mood
            });

            return "created";
        }

        // GET LAST 7 DAYS
        public async Task<List<DailyHealthDto>>
            GetLast7Days(string userId)
        {
            var collection =
                _firestore
                .Collection("DailyHealth")
                .Document(userId.Trim())
                .Collection("Records");

            var snapshot =
                await collection
                    .OrderByDescending("SortDate")
                    .GetSnapshotAsync();

            var result =
                new List<DailyHealthDto>();

            // ONLY LATEST 7 RECORDS
            foreach (var doc in snapshot.Documents.Take(7))
            {
                var data = doc.ToDictionary();

                result.Add(new DailyHealthDto
                {
                    Date =
                        data.ContainsKey("Date")
                        ? data["Date"]?.ToString()
                        : "",

                    WaterIntake =
                        data.ContainsKey("WaterIntake")
                        ? Convert.ToInt32(data["WaterIntake"])
                        : 0,

                    ExerciseDone =
                        data.ContainsKey("ExerciseDone")
                        ? Convert.ToBoolean(data["ExerciseDone"])
                        : false,

                    SleepHours =
                        data.ContainsKey("SleepHours")
                        ? Convert.ToInt32(data["SleepHours"])
                        : 0,

                    HealthyFood =
                        data.ContainsKey("HealthyFood")
                        ? Convert.ToBoolean(data["HealthyFood"])
                        : false,

                    Mood =
                        data.ContainsKey("Mood")
                        ? data["Mood"]?.ToString()
                        : ""
                });
            }

            return result;
        }
    }
}