using System;
using System.Net.Http.Json;
using TechNotes.Application.Notes;

namespace TechNotes.Client.Features.Notes;

public class NotesOverviewServiceClient : INotesOverviewService
{
    private readonly HttpClient _httpClient;

    public NotesOverviewServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<NoteResponse>?> GetNotesByCurrentUserAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<NoteResponse>>("api/notes");
    }

    public async Task<NoteResponse?> TogglePublishNoteAsync(int noteId)
    {
        var result = await _httpClient.PatchAsync($"api/notes/{noteId}", null);
        if (result is not null && result.Content is not null)
        {
            return await result.Content.ReadFromJsonAsync<NoteResponse>();
        }

        return null;
    }
}
