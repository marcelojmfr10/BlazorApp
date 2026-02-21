
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using TechNotes.Infrastructure.Users;

namespace BlazorApp.Features.Users.Components;

public partial class LogoutUser
{
    [Inject] private SignInManager<User> SignInManager { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await SignInManager.SignOutAsync();
        NavigationManager.NavigateTo("/notes");
    }
}