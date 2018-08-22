using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YLKnowledgeBase.Models;

namespace YLKnowledgeBase.ViewModels
{
    public class NoteVM
    {
        public Guid NoteId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreate { get; set; }

        //public Guid CategoriesList { get; set; }
        public Guid CategoryId { get; set; }
        public IEnumerable<Category> PossibleCategories { get; set; }

        //public IList<TagToCheckBoxViewModel> Tags { get; set; }

    }
}
