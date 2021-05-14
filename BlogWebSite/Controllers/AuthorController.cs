using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogWebSite.DataContract;
using BlogWebSite.Models;
using BlogWebSite.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlogWebSite.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private IGenericRepository<Author> _repository; // For Real database , repository pattern
        private BlogManagementContext context;
        private IConfiguration _config;

        public AuthorController(IConfiguration config)
        {
            context = new BlogManagementContext();
          //  _repository = new GenericRepository<Author>(context);
            _config = config;

        }

       
        [HttpPost]
        public IActionResult Authenticate([FromBody] AuthorModel author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = context.Authors.Where(e => e.UserName == author.UserName & e.Password == author.Password).SingleOrDefault();
                    if (user == null)
                        return BadRequest(new { message = "Username or password is incorrect" });

                    var jwtToken = GenerateJwtToken(user);

                    return Ok(new AuthenticateResponse
                    {
                        AuthorId = user.AuthorId,
                        UserName = user.UserName,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new { message = "Invalid Request" });
                }

            }
            catch (Exception e)
            {
                throw new ApplicationException("Error in Authentication:", e);
            }
        }

        private string GenerateJwtToken(Author user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(null,null,
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);



        }
    }
}
