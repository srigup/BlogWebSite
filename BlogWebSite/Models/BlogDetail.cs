using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BlogWebSite.Models
{
    public class BlogDetail
    {
        [Key]
        public int BlogId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Category { get; set; }
        [Required]
        public string BlogContent { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public virtual Author Author { get; set; }
    }
}
