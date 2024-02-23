using CodePruner.Examples.BackgroundTask.Hangfire;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

#region init_hangfire

builder.Services.AddHangfire(configuration => configuration
    .UseRecommendedSerializerSettings()
    .UseMemoryStorage());

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<SlowFileProcessor>();


var app = builder.Build();


#region init_hangfire_dashboard

app.UseHangfireDashboard();

#endregion



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region example_endpoints

app.MapGet("/ProcessLargeFileWithoutHangfire", async ([FromServices] SlowFileProcessor fileProcessor) =>
    {
        Console.WriteLine("[{0:HH:mm:ss}] Request start without Hangfire", DateTime.Now);
        var fileId = Random.Shared.Next(1, 1000);
        await new SlowFileProcessor().ProcessFileAsync(fileId);
        Console.WriteLine("[{0:HH:mm:ss}] Request end without Hangfire", DateTime.Now);
        return new FileProcessResult(fileId);
    })
    .WithName("ProcessLargeFileWithoutHangfire")
    .WithOpenApi();

app.MapGet("/ProcessLargeFileWithHangFire", ([FromServices] SlowFileProcessor fileProcessor) =>
    {
        Console.WriteLine("[{0:HH:mm:ss}] Request start with Hangfire", DateTime.Now);
        var fileId = Random.Shared.Next(1, 1000);
        BackgroundJob.Enqueue(() => fileProcessor.ProcessFileAsync(fileId));
        Console.WriteLine("[{0:HH:mm:ss}] Request end with Hangfire", DateTime.Now);
        return new FileProcessResult(fileId);
    })
    .WithName("ProcessLargeFileWithHangFire")
    .WithOpenApi();

#endregion
app.Run();

internal record FileProcessResult(int FileId);