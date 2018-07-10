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
            new Category { Name = "Kategoria 1", CategoryId= Guid.NewGuid() },
            new Category { Name = "Kategoria 2", CategoryId= Guid.NewGuid() }
        }; //test

        public IEnumerable<Category> GetAllCategories()
        {
            //return _context.Categories.ToList();
            return CategoriesTest; //test
        }

        public async Task<Category> GetCategory(Guid id)
        {
            //return await _context.Categories.SingleOrDefaultAsync(o => o.CategoryId == id);
            return CategoriesTest.ElementAt(0); //test
        }

        public bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(o => o.CategoryId == id);
        }

        public async void Save()
        {
            await _context.SaveChangesAsync();
        }

        public void CreateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void EditCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public void ToDeleteCategory()
        {
            throw new NotImplementedException();
        }
    }
}
