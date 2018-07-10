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
            new Category { Name = "Kategoria 1", CategoryId= new Guid("00000000-0000-0000-0000-000000000001") },
            new Category { Name = "Kategoria 2", CategoryId= new Guid("00000000-0000-0000-0000-000000000002") }
        }; //test

        public IEnumerable<Category> GetAllCategories()
        {
            //return _context.Categories.ToList();
            return CategoriesTest; //test
        }

        public Task <Category> GetCategory(Guid id)
        {
            return _context.Categories.SingleOrDefaultAsync(o => o.CategoryId == id);
            //return CategoriesTest.SingleOrDefault(o => o.CategoryId == id); //test
        }

        public bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(o => o.CategoryId == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
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

    }
}
