// Add services to the container.
using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Register database
builder.Services.AddDbContext<DataContext>(opt => 
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultCS"));
});
// Add Cors
builder.Services.AddCors();


// Configure the HTTP request pipeline.
var app = builder.Build();
// Use Cors
app.UseCors(x => {
    x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200");
});
app.MapControllers();
app.Run();
