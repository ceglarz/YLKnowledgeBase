using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YLKnowledgeBase.Models
{
    public class Category
    {
            [Display(Name = "Id:")]
            public int Id { get; set; }

            [Display(Name = "Nazwa kategorii")]
            [MaxLength(1500)]
            public string Name { get; set; }

            public virtual ICollection<Note> Note { get; set; }
    }
}
