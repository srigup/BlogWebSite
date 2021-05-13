using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogWebSite.Models;
using BlogWebSite.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebSite.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private IGenericRepository<Author> _repository;
        public AuthorController()
        {
            _repository = new GenericRepository<Author>(new BlogManagementContext());

        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] Author author)
        {
            try
            {
                var user = await _repository.Query(e => e.UserName == author.UserName & e.Password == author.Password);
                if (user.Any())
                {

                    var result = user.Select(x => x.UserName).ToString();


                    return Ok();
                }
                else
                {
                    throw new Exception();
                }
                
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error in Authentication, User Name or Password is incorrect :", e);
            }
        }


    }
}
