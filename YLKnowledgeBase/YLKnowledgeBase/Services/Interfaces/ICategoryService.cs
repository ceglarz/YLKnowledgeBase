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
        Task <Category> GetCategory(Guid? id);
        void CreateCategory(Category category);
        void EditCategory(Category category);
        void ToDeleteCategory();
        bool CategoryExists(Guid id);
        Task Save();
        void Update(Category category);
    }
}
