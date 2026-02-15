using BlazorApp.Components.Pages;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using TechNotes.Application.Notes.CreateNote;
using TechNotes.Application.Notes.DeleteNote;
using TechNotes.Application.Notes.GetNoteById;
using TechNotes.Application.Notes.UpdateNote;
using TechNotes.Infrastructure.Authentication;

namespace BlazorApp.Features.Notes.Components;

public partial class NoteEditor
{
    private string errorMessage = string.Empty;

    [SupplyParameterFromForm] private NoteModel? Note { get; set; }

    [Parameter] public int? NoteId { get; set; }

    [Inject] private ISender Sender { get; set; } = null!;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [Inject] private UserManager<User> UserManager { get; set; } = null!;

    private bool isEditMode => NoteId != null;

    [CascadingParameter] private HttpContext HttpContext { get; set; } = default!;

    protected override async Task OnParametersSetAsync()
    {
        if (NoteId is not null)
        {
            var result = await Sender.Send(new GetNoteByIdQuery { Id = (int)NoteId });
            if (result.IsSuccessful)
            {
                Note ??= result.Value.Adapt<NoteModel>();
                Note.Id = (int)NoteId;
            }
            else
            {
                SetErrorMessage(result.ErrorMessage);
            }
        }
        else
        {
            Note ??= new();
        }
    }

    private async Task HandleSubmit()
    {
        if (isEditMode)
        {
            var command = Note.Adapt<UpdateNoteCommand>();
            var result = await Sender.Send(command);
            if (result.IsSuccessful)
            {
                Note = result.Value.Adapt<NoteModel>();
                Console.WriteLine("nota actualizada");
                NavigationManager.NavigateTo("/notes");
            }
            else
            {
                SetErrorMessage(result.ErrorMessage);
            }
        }
        else
        {
            var command = Note.Adapt<CreateNoteCommand>();
            command.UserId = UserManager.GetUserId(HttpContext.User);
            var result = await Sender.Send(command);
            if (result.IsSuccessful)
            {
                Note = result.Adapt<NoteModel>();
                Console.WriteLine("nota creada");
                NavigationManager.NavigateTo("/notes");
            }
            else
            {
                SetErrorMessage(result.ErrorMessage);
            }
        }
    }

    private async Task DeleteNote()
    {
        if (NoteId is null)
        {
            return;
        }

        var command = new DeleteNoteCommand { Id = (int)NoteId };
        var result = await Sender.Send(command);
        if (result.IsSuccessful)
        {
            Console.WriteLine("nota borrada");
            NavigationManager.NavigateTo("/notes");
        }
        else
        {
            Console.WriteLine("error al borrar la nota");
        }
    }

    private void SetErrorMessage(string? error)
    {
        errorMessage = error ?? string.Empty;
    }
}