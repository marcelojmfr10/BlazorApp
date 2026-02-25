using System;

namespace TechNotes.Application.Notes;

public interface INotesOverviewService
{
    Task<NoteResponse?> TogglePublishNoteAsync(int noteId);
    Task<List<NoteResponse>?> GetNotesByCurrentUserAsync();
}
