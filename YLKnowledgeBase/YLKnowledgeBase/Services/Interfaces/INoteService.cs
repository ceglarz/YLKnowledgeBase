using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YLKnowledgeBase.Models;

namespace YLKnowledgeBase.Services.Interfaces
{
    public interface INoteService
    {
        Task<IEnumerable<Note>> GetAllNotes();
        Task<Note> GetNote(Guid? id);
        void CreateNote(Note note);
        void EditNote(Note note);
        void ToDeleteNote(Note note);
        bool NoteExists(Guid id);
        Task Save();
        void Update(Note note);
    }
}
