using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YLKnowledgeBase.Data;
using YLKnowledgeBase.Models;
using YLKnowledgeBase.Services;
using YLKnowledgeBase.Services.Interfaces;
using YLKnowledgeBase.ViewModels;

namespace YLKnowledgeBase.Controllers
{
    public class NotesController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly INoteService _noteService;
        private readonly ICategoryService _categoryService;
        public NotesController(INoteService noteService, ICategoryService categoryService)
        {
            _noteService = noteService;
            _categoryService = categoryService;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            return View(await _noteService.GetAllNotes());
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _noteService.GetNote(id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            NoteVMCreate notevm = new NoteVMCreate();
            //notevm.Tags = new List<TagToCheckBoxViewModel>();

            return View(notevm);
        }

        // POST: Notes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Content,DateOfCreate,CategoriesList")] NoteVMCreate notevm)
        {
            if (ModelState.IsValid)
            {
                Category category = await _categoryService.GetCategory(notevm.CategoriesList);
                var note = new Note {
                    //NoteId = Guid.NewGuid(),
                    Name = notevm.Name,
                    Content = notevm.Content,
                    DateOfCreate = notevm.DateOfCreate,
                    Category = category
                };

                //note.NoteId = Guid.NewGuid();
                await _noteService.CreateNote(note);
                return RedirectToAction(nameof(Index));
            }
            return View(notevm);
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _noteService.GetNote(id);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("NoteId,Name,Content,DateOfCreate")] Note note)
        public async Task<IActionResult> Edit(Guid id, [Bind("NoteId,Name,Content,DateOfCreate,CategoriesList")] NoteVMCreate notevm)
        {
            Note note = new Note { NoteId=notevm.NoteId, Name=notevm.Name , Content=notevm.Content, DateOfCreate=notevm.DateOfCreate };
            var category = await _categoryService.GetCategory(notevm.CategoriesList);
            note.Category = category;

            if (id != note.NoteId)
            {
                return NotFound();
            }

            //notevm.Category = await _categoryService.GetCategory( new Guid(CategoriesList) );
            //Note note = _noteService.CreateNote();
            //Note note = new Note();
            

            if (ModelState.IsValid)
            {
                
                try
                {
                    //_categoryService.EditCategory(category);
                    _noteService.EditNote(note);
                    _categoryService.Update(category);

                    await _noteService.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.NoteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _noteService.GetNote(id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        /*
        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var note = await _noteService.GetNote(id);
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */

        private bool NoteExists(Guid id)
        {
            return _noteService.NoteExists(id);
            //return _context.Notes.Any(e => e.NoteId == id);
        }
    }
}
