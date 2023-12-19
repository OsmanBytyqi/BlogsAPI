using Blogs.Core;
using Blogs.Core.Interfaces;
using Blogs.Core.Services;
using Blogs.Core.Utilities;
using Blogs.DB;
using Blogs.DB.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


var secret = Environment.GetEnvironmentVariable("JwtSettings__Secret") ?? "this is my private key so keep it secret";
var issuer = Environment.GetEnvironmentVariable("JwtSettings__Issuer") ?? " the blogs";

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
          .AddJwtBearer(opts =>
         {
             opts.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuerSigningKey = true,
                 ValidIssuer = issuer,
                 ValidateAudience = false,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret))
             };
         });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddTransient<IBlogsServices, BlogsServices>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

//concur 
//conquer

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
