using System;
using System.Collections.Generic;

#nullable disable

namespace BlogWebSite.Models
{
    public partial class BlogDetail
    {
        public int BlogId { get; set; }
        public string Tilte { get; set; }
        public string Category { get; set; }
        public string BlogContent { get; set; }
        public int AuthorId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public virtual Author Author { get; set; }
    }
}
