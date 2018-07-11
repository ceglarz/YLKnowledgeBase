using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("api/APINotes")]
    public class APINotesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly INoteService _notesService;
        public APINotesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public APINotesController(INoteService notesService)
        {
            _notesService = notesService;
        }

        // GET: api/APINotes
        /*[HttpGet]
        public IEnumerable<Note> Index()

        {
            var note = _notesService.GetAllNotes();

            return note;
        }*/

        // GET: api/APINotes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote([FromRoute] Guid? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _context.Notes.SingleOrDefaultAsync(m => m.NoteId == id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        // PUT: api/APINotes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote([FromRoute] Guid id, [FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.NoteId)
            {
                return BadRequest();
            }

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        // POST: api/APINotes
        [HttpPost]
        public async Task<IActionResult> PostNote([FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNote", new { id = note.NoteId }, note);
        }

        // DELETE: api/APINotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _context.Notes.SingleOrDefaultAsync(m => m.NoteId == id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return Ok(note);
        }

        private bool NoteExists(Guid id)
        {
            return _context.Notes.Any(e => e.NoteId == id);
        }
    }
}