using MediatR;
using Microsoft.AspNetCore.Components;
using TechNotes.Application.Notes;
using TechNotes.Application.Notes.GetNoteById;

namespace BlazorApp.Features.Notes.Components;

public partial class NoteView
{
    [Inject] private ISender Sender { get; set; } = null!;
    [Parameter] public int NoteId { get; set; }
    private NoteResponse? note;
    private string errorMessage = string.Empty;

    protected override async Task OnParametersSetAsync()
    {
        var result = await Sender.Send(
        new GetNoteByIdQuery { Id = NoteId });
        if (result.IsSuccessful && result.Value is not null)
        {
            note = (NoteResponse)result.Value;
        }
        else
        {
            errorMessage = result.ErrorMessage ?? "Lo sentimos, algo salió mal.";
        }
    }
}