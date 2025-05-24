using Sakila.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSakilaApplication(builder.Configuration);


var app = builder.Build();

app.AddSakilaApp();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();