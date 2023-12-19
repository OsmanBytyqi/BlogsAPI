using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Core.DTO
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public static explicit operator Blog(DB.Blog b) => new Blog
        {
            Id = b.Id,
            Description = b.Description,
            Title = b.Title
        };

    }
}
