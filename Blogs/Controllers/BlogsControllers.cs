using Blogs.Core.Interfaces;
using Blogs.Core.Services;
using Blogs.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace Blogs.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BlogsControllers : ControllerBase
    {
        private IBlogsServices _blogsService;

        public BlogsControllers(IBlogsServices blogsService)

        {
            _blogsService = blogsService;
        }
        [HttpGet]
        public IActionResult GetBlogs()
        {
            return Ok(_blogsService.GetBlogs());

        }

        [HttpGet("{id}", Name = "GetBlog")]
        public IActionResult GetBlog(int id)
        {
            return Ok(_blogsService.GetBlog(id));

        }
        [HttpPost]
        public IActionResult CreateBlog(DB.Blog blog)
        {
            var newBlog = _blogsService.CreateBlog(blog);
            return CreatedAtRoute("GetBlog", new { newBlog.Id }, newBlog);
        }

        //[HttpDelete]
        //public IActionResult DeleteBlog(Blog blog)
        //{
            //_blogsService.DeleteBlog(blog);
            //return Ok();
        //}


    }
}
