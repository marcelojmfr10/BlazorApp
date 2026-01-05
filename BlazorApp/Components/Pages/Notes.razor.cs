using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TechNotes.Application.Notes;
using TechNotes.Domain.Notes;

namespace BlazorApp.Components.Pages;

public partial class Notes
{
    private List<Note> notes = new();

    [Inject] private INoteService NoteService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(500);
        var result = NoteService.GetAllNotes();
        if(result is not null)
        {
            notes = result.ToList();
        }
    }
}