using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YLKnowledgeBase.Data;
using YLKnowledgeBase.Models;
using YLKnowledgeBase.Services.Interfaces;

namespace YLKnowledgeBase.Services
{
    public class NoteService : INoteService
    {
        private readonly ApplicationDbContext _context;

        public NoteService(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Note> GetAllNotes()
        {
            return _context.Notes.ToList();
        }

        public Note GetNotes(Guid id)
        {
            return _context.Notes.SingleOrDefault(o => o.NoteId == id);
        }

        public void CreateNote()
        {
            throw new NotImplementedException();
        }

        public void EditNote()
        {
            throw new NotImplementedException();
        }

        public void ToDeleteNote()
        {
            throw new NotImplementedException();
        }

        public bool NoteExists(Guid id)
        {
            return _context.Notes.Any(o => o.NoteId == id);
        }

        public async void Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
