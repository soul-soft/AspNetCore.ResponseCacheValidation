using ResponseCacheValidationSample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(a=>a.CacheProfiles.Add("ff",new Microsoft.AspNetCore.Mvc.CacheProfile 
{
}));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCacheValidation(configure => 
{
    configure.AddCacheValidator<WeatherForecastResponseCacheValidator>("WeatherForecast");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseResponseCacheValidation();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
