using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YLKnowledgeBase.Data;
using YLKnowledgeBase.Models;

namespace YLKnowledgeBase.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        List <Category> CategoriesTest = new List<Category>{
            new Category { Name = "Kategoria 1", CategoryId= new Guid("00000000-0000-0000-0000-000000000001"),
                Notes = new List<Note>(){ new Note() { NoteId = new Guid("00000000-0000-0000-0000-000000000003") },
                                            new Note() { NoteId = new Guid("00000000-0000-0000-0000-000000000004") } } },
            new Category { Name = "Kategoria 2", CategoryId= new Guid("00000000-0000-0000-0000-000000000002") }
        }; //test

        public IEnumerable<Category> GetAllCategories()
        {
            return (_context.Categories.ToList());
            /*return (await _context.Categories
                .Select(c => new
            {
                Category = c,
                Name = c.Name,
                Notes = c.Notes
            }).ToListAsync());*/
            //return CategoriesTest; //test
        }

        public async Task<Category> GetCategory(Guid? id)
        {
            var notes = await _context.Notes.Where(n => n.Category.CategoryId == id).ToListAsync();
            return await _context.Categories
                .Where(c => c.CategoryId == id)
                .Select(c => new Category { Name = c.Name, CategoryId = c.CategoryId, Notes = notes }).FirstOrDefaultAsync();
            //return await _context.Categories.SingleOrDefaultAsync(o => o.CategoryId == id);
            //return CategoriesTest.SingleOrDefault(o => o.CategoryId == id); //test
        }

        public void CreateCategory(Category category)
        {
            _context.Categories.Add(category);
        }

        public void EditCategory(Category category)
        {
            var toEdit = _context.Categories.Find(category.CategoryId);

            toEdit.Name = category.Name;
            toEdit.Notes = category.Notes;

            _context.Categories.Update(toEdit);
        }

        public void ToDeleteCategory()
        {
            throw new NotImplementedException();
        }

        public void Update(Category category)
        {
            _context.Update(category);
        }

        public bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(o => o.CategoryId == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
