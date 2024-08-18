using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")] // api/account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if(await UserExists(registerDto.Username))
            return BadRequest(new { Message = "Username is already taken." });

        return Ok();

        // using var hmac = new HMACSHA512();
        
        // var user = new AppUser 
        // {
        //     UserName = registerDto.Username.ToLower(),
        //     PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        //     PasswordSalt = hmac.Key
        // };

        // await context.Users.AddAsync(user);
        // await context.SaveChangesAsync();

        // var result = new UserDto
        // {
        //     Username = user.UserName,
        //     Token = tokenService.CreateToken(user)
        // };

        //return result;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginDto.Username.ToLower());
        if(user == null)
            return Unauthorized(new { Message = "Invalid username" });

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for(int i=0; i<computedHash.Length; i++)
        {
            if(computedHash[i] != user.PasswordHash[i])
                return Unauthorized(new { Message = "Invalid password" });
        }

        var result = new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };

        return result;
    }

    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
    }
}
