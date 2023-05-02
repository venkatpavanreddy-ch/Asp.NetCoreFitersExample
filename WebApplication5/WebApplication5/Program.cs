using WebApplication5.Models;
using Microsoft.Extensions.Configuration;
using WebApplication5.Common;
using WebApplication5.Filters;
using WebApplication5.Common.Services;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddScoped<ICryptoService, CryptoService>();
CryptoSettings cryptoSettings = new CryptoSettings
{
    AesCryptoKey = Configuration.GetValue<string>("CryptoSettings:AesCryptoKey"),
    AesCryptoIV = Configuration.GetValue<string>("CryptoSettings:AesCryptoIV")
};
builder.Services.AddSingleton(cryptoSettings);
builder.Services.AddControllers(options =>
{
    //Registering filters as global filters
    options.Filters.Add(new RequestBodyFilter(builder.Services.BuildServiceProvider().GetRequiredService<ICryptoService>()));
    options.Filters.Add(new JsonResultFilter(builder.Services.BuildServiceProvider().GetRequiredService<ICryptoService>()));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
AuthSettings authlSettings = new AuthSettings
{
    url = Configuration.GetValue<string>("AuthSettings:url"),
    audience = Configuration.GetValue<string>("AuthSettings:audience"),
    client_id = Configuration.GetValue<string>("AuthSettings:client_id"),
    client_secret = Configuration.GetValue<string>("AuthSettings:client_secret"),
    grant_type = Configuration.GetValue<string>("AuthSettings:grant_type"),
    scope = Configuration.GetValue<string>("AuthSettings:scope")
};
builder.Services.AddSingleton(authlSettings);




builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsApi",
    builder => builder.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CorsApi");
app.MapControllers();

app.Run();
