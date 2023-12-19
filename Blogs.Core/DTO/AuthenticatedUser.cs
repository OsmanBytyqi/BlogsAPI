using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Blogs.Core.DTO
{
    public class AuthenticatedUser
    {
        public string Token { get; set; }
        public string Username { get; set; }

    }
}
