using MediatR;
using Microsoft.AspNetCore.Components;
using TechNotes.Application.Users.AddRoleToUser;
using TechNotes.Application.Users.GetUserRoles;
using TechNotes.Application.Users.RemoveRoleFromUser;

namespace BlazorApp.Features.UserManagement.Components;

public partial class UserRolesModal
{
    [Inject] public ISender Sender { get; set; } = null!;
    [Parameter] public bool ShowModal { get; set; }
    [Parameter] public EventCallback<bool> ModalClosed { get; set; }
    [Parameter, EditorRequired] public required string UserId { get; set; }
    [Parameter] public string? UserName { get; set; }
    private List<string> Roles { get; set; } = [];
    private string newRole = string.Empty;

    protected override async Task OnParametersSetAsync()
    {
        await LoadUserRoles();
    }

    private async Task AddRole()
    {
        if (!string.IsNullOrWhiteSpace(newRole))
        {
            await Sender.Send(new AddRoleToUserCommand { UserId = UserId, RoleName = newRole });
            await LoadUserRoles();
            newRole = string.Empty;
        }
    }

    private async Task RemoveRole(string role)
    {
        await Sender.Send(new RemoveRoleFromUserCommand { UserId = UserId, RoleName = role });
        await LoadUserRoles();
    }

    private void CloseModal()
    {
        ShowModal = false;
        newRole = string.Empty;
        ModalClosed.InvokeAsync(ShowModal);
    }

    private async Task LoadUserRoles()
    {
        if (ShowModal && UserId is not null)
        {
            var result = await Sender.Send(new GetUserRolesQuery { UserId = UserId });
            if (result.IsSuccessful && result.Value is not null)
            {
                Roles = result.Value;
            }
            else
            {
                Roles = [];
            }
        }
    }
}