using MediatR;
using Microsoft.AspNetCore.Components;
using TechNotes.Application.Notes;
using TechNotes.Application.Notes.GetNotes;
using TechNotes.Domain.Notes;

namespace BlazorApp.Components.Pages;

public partial class Notes
{
    private List<Note> notes = new();

    // [Inject] private INoteService NoteService { get; set; } = null!;
    [Inject] private ISender Sender {get; set;} = null!;

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(500);
        var result = await Sender.Send(new GetNotesQuery()); //NoteService.GetAllNotesAsync();
        if(result is not null)
        {
            notes = result.ToList();
        }
    }
}