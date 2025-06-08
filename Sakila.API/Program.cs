using Sakila.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationLayer(builder.Configuration, builder.Environment);


var app = builder.Build();

app.AddWebApplicationLayer();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();