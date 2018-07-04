using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YLKnowledgeBase.Models
{
    public class Note
    {
        public Note()
        {
            this.Tags = new HashSet<Tag>();
        }

        [Key]
        [Display(Name = "Id")]
        public Guid NoteId { get; set; }
        public string Name { get; set; }
        [Display(Name = "Content of the note")]
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
