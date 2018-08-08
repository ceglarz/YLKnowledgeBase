using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YLKnowledgeBase.Data;
using YLKnowledgeBase.Models;
using YLKnowledgeBase.Services;
using YLKnowledgeBase.Services.Interfaces;

namespace YLKnowledgeBase.Controllers
{
    [Produces("application/json")]
    [Route("api/Notes")]
    public class APINotesController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly INoteService _noteService;
        public APINotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        // GET: api/Notes
        [HttpGet]
        public async Task <IEnumerable<Note>> GetNotes()
        {
            return await _noteService.GetAllNotes();
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _noteService.GetNote(id);
            //var category = await _context.Categories.SingleOrDefaultAsync(m => m.CategoryId == id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        // PUT: api/Notes/5
        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> PutCategory([FromRoute] Guid id, [FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.NoteId)
            {
                return BadRequest();
            }

            _noteService.EditNote(note);
            //_context.Entry(category).State = EntityState.Modified;

            try
            {
                await _noteService.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Notes
        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _noteService.CreateNote(note);
            await _noteService.Save();

            return CreatedAtAction("GetNote", new { id = note.NoteId }, note);
        }

        private bool NoteExists(Guid id)
        {
           return _noteService.NoteExists(id);
           //return _context.Notes.Any(e => e.NoteId == id);
        }
    }
}