using BlazorApp.Components.Pages;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Components;
using TechNotes.Application.Notes.CreateNote;
using TechNotes.Application.Notes.DeleteNote;
using TechNotes.Application.Notes.GetNoteById;
using TechNotes.Application.Notes.UpdateNote;

namespace BlazorApp.Features.Notes.Components;

public partial class NoteEditor
{
    [SupplyParameterFromForm]
    private NoteModel? Note { get; set; }

    [Parameter]
    public int? NoteId { get; set; }

    [Inject]
    private ISender Sender { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;


    private bool isEditMode => NoteId != null;

    protected override async Task OnParametersSetAsync()
    {
        if(NoteId is not null)
        {
            var result = await Sender.Send(new GetNoteByIdQuery { Id = (int)NoteId });
            if(result is not null)
            {
                Note ??= result.Adapt<NoteModel>();
                Note.Id = (int)NoteId;
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
            if(result is not null)
            {
                Note = result.Adapt<NoteModel>();
            }
            Console.WriteLine("nota actualizada");
        } 
        else
        {
            var command = Note.Adapt<CreateNoteCommand>();
            var result = await Sender.Send(command);
            Note = result.Adapt<NoteModel>();
            Console.WriteLine("nota creada");
        }
    }

    private async Task DeleteNote()
    {
        if(NoteId is null)
        {
            return;
        }

        var command = new DeleteNoteCommand { Id = (int)NoteId };
        var isSuccess = await Sender.Send(command);
        if (isSuccess)
        {
            Console.WriteLine("nota borrada");
            NavigationManager.NavigateTo("/notes");
        } 
        else
        {
            Console.WriteLine("error al borrar la nota");
        }
    }
}