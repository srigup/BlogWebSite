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
        private IGenericRepository<Author> _repository;
        private IConfiguration _config;

        public AuthorController(IConfiguration config)
        {
            _repository = new GenericRepository<Author>(new BlogManagementContext());
            _config = config;

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthorModel author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _repository.Query(e => e.UserName == author.UserName & e.Password == author.Password);
                    if (user == null)
                        return BadRequest(new { message = "Username or password is incorrect" });
                    // return Ok(user);
                    var jwtToken = GenerateJwtToken(user.Single());

                    return Ok(new AuthenticateResponse
                    {
                        AuthorId = user.Single().AuthorId,
                        UserName = user.Single().UserName,
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
