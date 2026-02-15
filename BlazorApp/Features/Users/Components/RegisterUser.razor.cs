using MediatR;
using Microsoft.AspNetCore.Components;
using TechNotes.Application.Users.RegisterUser;

namespace BlazorApp.Features.Users.Components;

public partial class RegisterUser
{
    [Inject] private ISender Sender { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [SupplyParameterFromForm]
    private RegisterUserModel UserModel { get; set; } = new();
    private string errorMessage = string.Empty;

    async Task HandleSubmit()
    {
        var command = new RegisterUserCommand
        {
            UserName = UserModel.UserName,
            UserEmail = UserModel.Email,
            Password = UserModel.Password
        };

        var result = await Sender.Send(command);
        if (result.IsSuccessful)
        {
            NavigationManager.NavigateTo("/login");
        }
        else
        {
            errorMessage = result.ErrorMessage ?? "Ha ocurrido un error en el registro.";
        }
    }
}