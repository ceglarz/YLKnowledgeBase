using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        List<Note> NotesTest = new List<Note>{
            new Note {
                Name = "Notatka 1",
                Content = "bla bla",
                Category = new Category(){CategoryId = new Guid("00000000-0000-0000-0000-000000000001"), Name="Kategoria 1"},
                NoteId = new Guid("00000000-0000-0000-0000-000000000003"), TagNotes = new List<TagNote>(){}
                /*TagNotes= new List<TagNote>(){
                    TagId = new Guid("00000000-0000-0000-0000-000000000004"), Tag= new Tag(),
                    NoteId = new Guid("00000000-0000-0000-0000-000000000003"), Note= new Note() }*/ }
        }; //test

        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            return (await _context.Notes.ToListAsync());
            //return (await _context.Notes.Include(note => note.Category).ToListAsync());
            //return CategoriesTest; //test
        }

        public async Task<Note> GetNote(Guid? id)
        {
            var category = await _context.Categories.Where(n => n.CategoryId == id).FirstOrDefaultAsync();
            return await _context.Notes
                .Where(c => c.NoteId == id)
                .Select(n => new Note() { NoteId = n.NoteId, Name = n.Name, Content = n.Content, DateOfCreate = n.DateOfCreate,
                    Category = new Category() { CategoryId = n.Category.CategoryId, Name = n.Category.Name, Notes = n.Category.Notes }
                })
                .FirstOrDefaultAsync();
            //return await _context.Notes.Where(o => o.NoteId == id).FirstOrDefaultAsync();
            //return NotesTest.SingleOrDefault(o => o.NoteId == id); //SingleOrDefault(o => o.NoteId == id); //test
        }

        public async Task CreateNote(Note note)
        {
            note.Category = _context.Categories.Where(c => c.CategoryId == note.Category.CategoryId).FirstOrDefault();
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
        }

        public void EditNote(Note note)
        {
            var toEdit = _context.Notes.Find(note.NoteId);

            toEdit.Name = note.Name;
            toEdit.Category = note.Category;
            toEdit.Content = note.Content;

            _context.Notes.Update(toEdit);
        }

        public void ToDeleteNote()
        {
            throw new NotImplementedException();
        }

        public void Update(Note note)
        {
            _context.Update(note);
        }

        public bool NoteExists(Guid id)
        {
            return _context.Notes.Any(o => o.NoteId == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
