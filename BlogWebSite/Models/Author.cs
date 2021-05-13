using System;
using System.Collections.Generic;

#nullable disable

namespace BlogWebSite.Models
{
    public partial class Author
    {
        public Author()
        {
            BlogDetails = new HashSet<BlogDetail>();
        }

        public int AuthorId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<BlogDetail> BlogDetails { get; set; }
    }
}
