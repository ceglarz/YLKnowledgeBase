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
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Content of the note")]
        [StringLength(5000, MinimumLength = 3)]
        [Required]
        public string Content { get; set; }
        public DateTime DateOfCreate { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<TagNote> TagNotes { get; set; }
    }
}
