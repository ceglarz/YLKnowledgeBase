using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YLKnowledgeBase.Models;

namespace YLKnowledgeBase.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategory(Guid id);
        void CreateCategory();
        void EditCategory();
        void ToDeleteCategory();
        bool CategoryExists(Guid id);
        void Save();
    }
}
