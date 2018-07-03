using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YLKnowledgeBase.Models
{
    public class Note
    {
            [Display(Name = "Id:")]
            public int Id { get; set; }

            [Display(Name = "Treść wpisu:")]
            [MaxLength(1500)]
            public string Description { get; set; }

            public virtual Category Category { get; set; }
    }
}
