using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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


        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();

            return new List<Category>
            {
                new Category("Kategoria 1"), new Category("Kategoria 2")
            };
        }

        public Category GetCategory(Guid id)
        {
            return _context.Categories.SingleOrDefault(o => o.CategoryId == id);
        }

        public void CreateCategory()
        {
            throw new NotImplementedException();
        }

        public void EditCategory()
        {
            throw new NotImplementedException();
        }

        public void ToDeleteCategory()
        {
            throw new NotImplementedException();
        }

        public bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(o => o.CategoryId == id);
        }

        public async void Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
