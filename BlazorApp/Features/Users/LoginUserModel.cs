using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Features.Users;

public class LoginUserModel
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
