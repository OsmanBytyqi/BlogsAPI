using Blogs.Core.DTO;
using System.Reflection.Metadata;
namespace Blogs.Core.Interfaces
{
    public interface IBlogsServices
    {
        List<Blog> GetBlogs();
        Blog GetBlog(int id);
        Blog CreateBlog(DB.Blog blog);
        void DeleteBlog(Blog blog);
        Blog EditBlog(Blog blog);
        void DeleteBlog(DB.Blog blog);
    }
}
