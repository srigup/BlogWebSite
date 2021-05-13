using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebSite.Models;
using BlogWebSite.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private IGenericRepository<BlogDetail> _repository;

        public BlogController()
        {
            _repository = new GenericRepository<BlogDetail>(new BlogManagementContext());

        }

       
        [HttpGet]
        [Route("GetAllBlog")]
        public async Task<IActionResult> GetAllBlog()
        {
            try
            {
                List<BlogDetail> listOfBlogs = new List<BlogDetail>();
                listOfBlogs = await _repository.GetAll();
                return Ok(listOfBlogs);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error in Get All Blogs :", e);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("CreateBlog")]
        public async Task<IActionResult> CreateBlog([FromBody] BlogDetail blogDetail)
        {
            try
            {
                 var result = await _repository.InsertModel(blogDetail);
                    return Ok();
                
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error in Posting Blog :", e);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [Route("UpdateBlog")]
        public async Task<IActionResult> UpdateBlog([FromBody] BlogDetail blogDetail)
        {
            try
            {
                using (var db = new BlogManagementContext())
                {
                    db.BlogDetails.Attach(blogDetail);
                    db.Entry(blogDetail).Property(x => x.BlogContent).IsModified = true;
                    db.Entry(blogDetail).Property(x => x.Tilte).IsModified = true;
                    db.Entry(blogDetail).Property(x => x.Category).IsModified = true;
                    db.SaveChanges();
                }
                return Ok();
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error in Updating Blog :", e);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("DeleteBlog/{id}")]
        public async Task<IActionResult> DeleteBlog([FromRoute]int id)
        {
            try
            {
                await _repository.DeleteModel(id);
                return Ok();
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error in Deleting Blog :", e);
            }
        }

    }
}
