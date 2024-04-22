using CodePruner.Examples.SignalR.Api.SignalRCode;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod().
                AllowCredentials();
        });
});
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();


app.MapHub<ProcessingHub>("/ProcessingHub");
app.MapHub<StronglyTypedProcessingHub>("/StronglyTypedProcessingHub");

app.MapPost("/StartFileProcessing", () =>
{
    var hub = app.Services.GetRequiredService<IHubContext<ProcessingHub>>();
    var fileProcessingId = Guid.NewGuid();
    return new FileProcessId(fileProcessingId);
   
})
.WithName("FileProcessing")
.WithOpenApi();

app.Run();

internal record FileProcessId(Guid Id);
