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

// Configure the HTTP request pipeline.
var app = builder.Build();
app.MapControllers();
app.Run();
