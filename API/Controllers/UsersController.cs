using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController(DataContext context) : BaseApiController
{    
    [HttpGet] // api/users
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await context.Users.ToListAsync();           
        
        return users;
    }

    [HttpGet("{id:int}")] // api/users/1
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return NotFound(new { Message = $"Not found with id={id}" });
        
        return user;
    }
}
