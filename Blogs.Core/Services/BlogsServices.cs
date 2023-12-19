using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs.Core.DTO;
using Blogs.Core.Interfaces;
using Blogs.DB.Models;

namespace Blogs.Core.Services
{

    public class BlogsServices : IBlogsServices
    {
        private DB.AppDbContext _context;
        private readonly User _user;

        public BlogsServices(DB.AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users
             .First(u => u.Username == httpContextAccessor.HttpContext.User.Identity.Name);

        }

        public Blog CreateBlog(DB.Blog blog)
        {
            blog.User = _user;
            _context.Add(blog);
            _context.SaveChanges();

            return (Blog)blog;
        }

        public Blog EditBlog(Blog blog)
        {
            var dbBlog = _context.Blogs
                 .Where(b => b.User.Id == _user.Id && b.Id == blog.Id)
                 .First();
            dbBlog.Description = blog.Description;
            dbBlog.Title = blog.Title;
            _context.SaveChanges();

            return blog;
        }



        public Blog GetBlog(int id) =>
             _context.Blogs
                .Where(e => e.User.Id == _user.Id && e.Id == id)
                .Select(e => (Blog)e)
                .First();


        public List<Blog> GetBlogs() =>
            _context.Blogs
                .Where(e => e.User.Id == _user.Id)
                .Select(e => (Blog)e)
                .ToList();

        public void DeleteBlog(Blog blog)
        {
            var dbBlog = _context.Blogs.First(e => e.User.Id == _user.Id && e.Id == blog.Id);
            _context.Remove(dbBlog);
            _context.SaveChanges();
        }
    }


}
