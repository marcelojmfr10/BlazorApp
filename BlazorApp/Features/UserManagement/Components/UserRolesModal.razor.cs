using Microsoft.AspNetCore.Components;

namespace BlazorApp.Features.UserManagement.Components;

public partial class UserRolesModal
{
    [Parameter] public bool ShowModal { get; set; }
    [Parameter] public EventCallback<bool> ModalClosed { get; set; }
    [Parameter, EditorRequired] public required string UserId { get; set; }
    [Parameter] public string? UserName { get; set; }
    private List<string> Roles { get; set; } = [];
    private string newRole = string.Empty;

    protected override async Task OnParametersSetAsync()
    {
        Console.WriteLine($"mostrando modal para usuario {UserName} {UserId}");
        if (ShowModal && UserId is not null)
        {
            Roles = new List<string> { "test role 1", "test role 2" };
        }
    }

    private async Task AddRole()
    {
        if (!string.IsNullOrWhiteSpace(newRole))
        {
            Roles.Add(newRole);
            newRole = string.Empty;
        }
    }

    private async Task RemoveRole(string role)
    {
        Roles.Remove(role);
    }

    private void CloseModal()
    {
        ShowModal = false;
        newRole = string.Empty;
        ModalClosed.InvokeAsync(ShowModal);
    }
}