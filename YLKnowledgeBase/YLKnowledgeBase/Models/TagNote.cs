using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YLKnowledgeBase.Models
{
    public class TagNote
    {
        public Guid TagId { get; set; }
        public Guid NoteId { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual Note Note { get; set; }
    }
}
