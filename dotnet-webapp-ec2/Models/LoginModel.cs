using System.ComponentModel.DataAnnotations;

namespace dotnet_webapp_ec2.Models;

public class LoginModel
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
