using Blogs.Core.DTO;
using Blogs.Core.Utilities;
using Blogs.DB.Models;
namespace Blogs.Core.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticatedUser> SignUp(User user);
        Task<AuthenticatedUser> SignIn(User user);

    }
}
