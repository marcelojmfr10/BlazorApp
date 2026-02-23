using MediatR;
using Microsoft.AspNetCore.Components;
using TechNotes.Application.Users;
using TechNotes.Application.Users.GetUsers;

namespace BlazorApp.Features.UserManagement.Components;

public partial class UserOverview
{
    [Inject] public ISender Sender { get; set; } = null!;
    private List<UserResponse> users = [];
    private bool showModal = false;
    private string selectedUserId = string.Empty;
    private string selectedUserName = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        var result = await Sender.Send(new GetUsersQuery());
        if (result.IsSuccessful && result.Value is not null)
        {
            users = result.Value;
        }
        else
        {
            users = new();
        }
    }

    private void OpenModal(string userId, string userName)
    {
        selectedUserId = userId;
        selectedUserName = userName;
        showModal = true;
        Console.WriteLine($"modal abierto con usuario {userName}");
    }

    private void CloseModal()
    {
        selectedUserId = string.Empty;
        selectedUserName = string.Empty;
        showModal = false;
        Console.WriteLine($"modal cerrado");
    }
}