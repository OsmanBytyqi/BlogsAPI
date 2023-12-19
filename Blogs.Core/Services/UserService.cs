using Blogs.Core.CustomExceptions;
using Blogs.Core.DTO;
using Blogs.Core.Interfaces;
using Blogs.Core.Utilities;
using Blogs.DB;
using Blogs.DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices.Marshalling;

namespace Blogs.Core.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(AppDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;

        }

        public async Task<AuthenticatedUser> SignUp(User user)
        {
            var checkUsername = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.Equals(user.Username));

            if (checkUsername != null)
            {
                throw new UsernameAlreadyExistsException("Username already exists");
            }

            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = _passwordHasher.HashPassword(user, user.Password);
            }

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return new AuthenticatedUser
            {
                Username = user.Username,
                Token = JwtGenerator.GenerateAuthToken(user.Username)
            };
        }
        public async Task<AuthenticatedUser> SignIn(User user)
        {
            var dbUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == user.Username);

            if (dbUser == null || dbUser.Password == null || _passwordHasher.VerifyHashedPassword(dbUser, dbUser.Password, user.Password) == PasswordVerificationResult.Failed)
            {
                throw new InvalidUsernamePasswordException("Invalid username or password");
            }

            return new AuthenticatedUser()
            {
                Username = user.Username,
                Token = JwtGenerator.GenerateAuthToken(user.Username),
            };
        }

    }
}
