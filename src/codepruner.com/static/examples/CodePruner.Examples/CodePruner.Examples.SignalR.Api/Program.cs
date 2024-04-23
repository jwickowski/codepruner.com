using System.Text.Json.Serialization;
using CodePruner.Examples.SignalR.Api.Processing;
using CodePruner.Examples.SignalR.Api.SignalRCode;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddSignalR()
    .AddJsonProtocol(options => options
        .PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        corsPolicyOptions =>
        {
            corsPolicyOptions.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod().
                AllowCredentials();
        });
});

builder.Services.AddSingleton<FileProcessingStore>();
builder.Services.AddTransient<FileProcessor>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.MapHub<ProcessingHub>("/ProcessingHub");
app.MapHub<StronglyTypedProcessingHub>("/StronglyTypedProcessingHub");

app.MapPost("/StartFileProcessingSync", async (
        FileProcessor fileProcessor,
        IHubContext<StronglyTypedProcessingHub, IProcessingClient> hub) =>
    {
        var fileProcessingId = Guid.NewGuid();
        var fileProcessId = new FileProcessId(fileProcessingId);

        var actualState = new ProcessStatusState(fileProcessId, ProcessStatus.InQueue))
        while (actualState.Status != ProcessStatus.Done)
        {
            await hub.Clients.All.ProcessStatusUpdate(new ProcessStatusUpdateMessage(fileProcessId.Id,
                actualState.Status));
            await Task.Delay(2000);
            actualState = await fileProcessor.ProcessFile(fileProcessId, actualState.Status);
        }

        await hub.Clients.All.ProcessStatusUpdate(new ProcessStatusUpdateMessage(fileProcessId.Id, actualState.Status));
    })
    .WithName("FileProcessing")
    .WithOpenApi();

app.Run();