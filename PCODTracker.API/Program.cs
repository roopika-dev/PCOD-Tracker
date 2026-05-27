using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using PCODTracker.API.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add Controllers
builder.Services.AddControllers();

// 🔹 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔥 Firebase Initialization
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential
        .FromFile(
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "Firebase",
                "firebase-key.json"))
        .CreateScoped(
            "https://www.googleapis.com/auth/cloud-platform")
});

// 🔥 Firestore DB
builder.Services.AddSingleton(provider =>
{
    return new FirestoreDbBuilder
    {
        ProjectId = "pcod-tracker-a838d",

        Credential = GoogleCredential.FromFile(
            Path.Combine(
                Directory.GetCurrentDirectory(),
                "Firebase",
                "firebase-key.json"))
    }
    .Build();
});

// 🔥 Register Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<DailyHealthService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// 🔹 Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();