using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#nullable disable

namespace BlogWebSite.Models
{
    public class Author
    {
        public Author()
        {
            BlogDetails = new HashSet<BlogDetail>();
        }

        [Key]
        public int AuthorId { get; set; }
        public string UserName { get; set; }
       
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<BlogDetail> BlogDetails { get; set; }
    }
}
