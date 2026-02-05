using Mapster;
using MediatR;
using Microsoft.AspNetCore.Components;
using TechNotes.Application.Notes.CreateNote;

namespace BlazorApp.Components.Pages
{
    public partial class NoteEditor
    {
        [SupplyParameterFromForm]
        private NoteModel Note { get; set; } = new NoteModel();

        [Inject]
        private ISender Sender { get; set; } = null!;

        private async Task HandleSubmit()
        {
            var command = Note.Adapt<CreateNoteCommand>();
            var result = await Sender.Send(command);
            Note = result.Adapt<NoteModel>();
            Console.WriteLine("nota creada");
        }
    }
}