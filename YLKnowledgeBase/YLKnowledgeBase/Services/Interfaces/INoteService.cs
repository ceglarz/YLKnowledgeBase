using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YLKnowledgeBase.Models;

namespace YLKnowledgeBase.Services.Interfaces
{
    public interface INoteService
    {
        IEnumerable<Note> GetAllNotes();
        Note GetNotes(Guid? id);
        void CreateNote();
        void EditNote(Note note);
        void ToDeleteNote();
        bool NoteExists(Guid id);
        void Save();
    }
}
