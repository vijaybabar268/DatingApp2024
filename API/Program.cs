using API;
using API.Extensions;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);


// Configure the HTTP request pipeline.
var app = builder.Build();
// Exception middleware
app.UseMiddleware<ExceptionMiddleware>();
// Use Cors
app.UseCors(x => {
    x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200");
});
// Use Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
