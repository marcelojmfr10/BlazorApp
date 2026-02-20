using MediatR;
using Microsoft.AspNetCore.Components;
using TechNotes.Application.Users.LoginUser;

namespace BlazorApp.Features.Users.Components
{
    public partial class LoginUser
    {
        [Inject] private ISender Sender { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        [SupplyParameterFromForm]
        private LoginUserModel LoginUserModel { get; set; } = new();
        private string errorMessage = string.Empty;

        protected override void OnInitialized()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            if (Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query).TryGetValue("error", out var error))
            {
                errorMessage = error.ToString();
            }
        }

        async Task HandleSubmit()
        {
            var command = new LoginUserCommand
            {
                UserName = LoginUserModel.UserName,
                Password = LoginUserModel.Password
            };

            var result = await Sender.Send(command);
            if (result.IsSuccessful)
            {
                NavigationManager.NavigateTo("/notes");
            }
            else
            {
                errorMessage = result.ErrorMessage ?? "Ha ocurrido un error en el inicio de sesión.";
            }
        }
    }
}