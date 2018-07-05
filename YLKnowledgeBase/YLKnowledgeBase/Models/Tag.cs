using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YLKnowledgeBase.Models
{
    public class Tag
    {
        [Key]
        [Display(Name = "Id")]
        public Guid TagId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TagNote> TagNotes { get; set; }
    }
}
