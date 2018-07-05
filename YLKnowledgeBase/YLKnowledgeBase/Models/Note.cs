using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YLKnowledgeBase.Models
{
    public class Note
    {
        [Key]
        [Display(Name = "Id")]
        public Guid NoteId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [Display(Name = "Content of the note")]
        public string Content { get; set; }
        public DateTime DateOfCreate { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<TagNote> TagNotes { get; set; }
    }
}
