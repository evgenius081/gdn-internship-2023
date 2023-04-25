using System.Net.Http.Headers;
using BankApp.API.Interfaces;
using BankApp.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
var apiPath = builder.Configuration.GetValue<string>("BankAPIPath");
builder.Services.AddHttpClient("NBP", httpClient =>
{
    httpClient.BaseAddress = new Uri(apiPath!);
    httpClient.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});
builder.Services.AddScoped<IBankService, BankService>();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));

app.MapControllers();

app.Run();
