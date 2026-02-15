using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Features.Users;

public class RegisterUserModel
{
    [Required(ErrorMessage = "Nombre de usuario requerido")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email requerido")]
    [EmailAddress(ErrorMessage = "Email de usuario invalido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Contraseña requerida")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirmación de Contraseña requerida")]
    [Compare("Password", ErrorMessage = "La contraseña y confirmación no coinciden")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
