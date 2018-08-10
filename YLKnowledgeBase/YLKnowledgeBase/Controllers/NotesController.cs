using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YLKnowledgeBase.Data;
using YLKnowledgeBase.Models;
using YLKnowledgeBase.Services;
using YLKnowledgeBase.Services.Interfaces;

namespace YLKnowledgeBase.Controllers
{
    public class NotesController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
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
        public async Task<IActionResult> Create()
        {
            var httpClient = new HttpClient();
            var jsonData = await httpClient.GetStringAsync("https://localhost:44306/api/Categories");
            var data = JsonConvert.DeserializeObject<IEnumerable<Category>>(jsonData);
            ViewBag.Data = data;
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoteId,Name,Content,DateOfCreate,CategoryId")] Note note)
        {
            if (ModelState.IsValid)
            {
                note.NoteId = Guid.NewGuid();
                _noteService.CreateNote(note);
                await _noteService.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(note);
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
            var httpClient = new HttpClient();
            var jsonData = await httpClient.GetStringAsync("https://localhost:44306/api/Categories");
            var data = JsonConvert.DeserializeObject<IEnumerable<Category>>(jsonData);
            ViewBag.Data = data;
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("NoteId,Name,Content,DateOfCreate,Category.CategoryId")] Note note)
        {
            if (id != note.NoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _noteService.Update(note);
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

        
        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var note = await _noteService.GetNote(id);
            _noteService.ToDeleteNote(note);
            await _noteService.Save();
            return RedirectToAction(nameof(Index));
        }
        

        private bool NoteExists(Guid id)
        {
            return _noteService.NoteExists(id);
            //return _context.Notes.Any(e => e.NoteId == id);
        }
    }
}