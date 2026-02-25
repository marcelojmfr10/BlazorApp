using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using TechNotes.Application.Notes;

namespace TechNotes.Client.Features.Notes.Components;

public partial class NoteOverview
{
    [Inject] public INotesOverviewService NotesOverviewServiceClient { get; set; } = null!;
    [Inject] NavigationManager NavigationManager { get; set; } = null!;
    private List<NoteResponse>? notes = [];

    protected override async Task OnInitializedAsync()
    {
        var result = await NotesOverviewServiceClient.GetNotesByCurrentUserAsync();
        if (result != null)
        {
            notes = result;
        }
        else
        {
            notes = [];
        }
    }

    private void EditNote(int id)
    {
        NavigationManager.NavigateTo($"/note-editor/{id}");
    }

    private async Task TogglePublishNote(int noteId)
    {
        var updatedNote = await NotesOverviewServiceClient.TogglePublishNoteAsync(noteId);
        if (updatedNote is not null && notes is not null)
        {
            var index = notes.FindIndex(note => note.Id == noteId);
            if (index != -1)
            {
                notes[index] = (NoteResponse)updatedNote;
                StateHasChanged();
            }
            else
            {
                Console.Error.WriteLine($"Error al actualizar el estado de la nota");
            }
        }
    }
}