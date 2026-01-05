using TechNotes.Domain.Notes;

namespace TechNotes.Application.Notes;

public class NoteService : INoteService
{
    public List<Note> GetAllNotes()
    {
        return new List<Note> { 
            new() { Id = 1, Title = "Primer nota", Content = "Contenido de primer nota", IsPublished = true, PublishedAt = DateTime.UtcNow, CreatedAt = DateTime.UtcNow },
         new() { Id = 2, Title = "Segunda nota", Content = "Contenido de segunda nota", IsPublished = true, PublishedAt = null, CreatedAt = DateTime.UtcNow }
            };
    }
}
