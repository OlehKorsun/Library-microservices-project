using Api.Workers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Application.Settings;
using Infrastructure.Messaging;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using SendGrid;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton(new RabbitMqConnection("127.0.0.1", "guest", "guest"));

builder.Services.Configure<Infrastructure.MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddScoped<INotificationRepository, MongoNotificationRepository>();
builder.Services.AddScoped<NotificationOrchestrator>();

builder.Services.AddScoped<INotificationRepository, MongoNotificationRepository>();

builder.Services.AddSingleton<EventPublisher>();
builder.Services.AddHostedService<RabbitMqConsumer>();

// SEND GRID
var sendGridSection = builder.Configuration.GetSection("SendGridSettings");
builder.Services.Configure<SendGridSettings>(sendGridSection);

var apiKey = sendGridSection["ApiKey"]; 
builder.Services.AddSingleton<ISendGridClient>(new SendGridClient(apiKey));

builder.Services.AddScoped<IEmailSenderService, SendGridEmailSender>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// --- TEST ENDPOINT ---
app.MapPost("/test-notify", async ([FromServices]EventPublisher publisher) =>
{
    var fakeEvent = new Application.DTOs.BookOverdueDto()
    {
        Id = 1,
        Email = "user@example.com",
        BookTitle = "Война и Мир",
        DueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1))
    };

    
    await publisher.PublishAsync("notification_queue", fakeEvent);
    return Results.Ok("Event sent!");
});

app.UseHttpsRedirection();

app.Run();