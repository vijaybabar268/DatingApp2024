using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class RegisterDto
{   
    [Required] 
    public string Username { get; set; } = string.Empty;

    [Required]
    [MinLength(4), MaxLength(8)]
    public string Password { get; set; } = string.Empty;
}
