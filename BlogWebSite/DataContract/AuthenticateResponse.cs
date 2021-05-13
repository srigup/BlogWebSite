using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebSite.DataContract
{
    public class AuthenticateResponse
    {
        public string Token { get; set; }
        public int AuthorId { get; set; }
        public string UserName { get; set; }
    }
}
