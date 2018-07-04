﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YLKnowledgeBase.Models
{
    public class Category
    {

        public Category()
        {
            this.Notes = new HashSet<Note>();
        }

        [Key]
        [Display(Name = "Id")]
        public Guid CategoryId { get; set; }
        [Display(Name = "Category name")]
        [MaxLength(1500)]
        public string Name { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}
