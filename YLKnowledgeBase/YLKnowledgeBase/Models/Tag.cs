using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YLKnowledgeBase.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Notes = new HashSet<Note>();
        }

        [Key]
        [Display(Name = "Id")]
        public Guid TagId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}
