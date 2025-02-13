using DattingAppApi.Data;
using DattingAppApi.DTOs;
using DattingAppApi.Entities;
using DattingAppApi.Interfaces;
using DattingAppApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DattingAppApi.Controllers
{
    public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto) 
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is already taken");

            return Ok();

            //using var hmac = new HMACSHA512();

            //var user = new AppUser
            //{
            //    UserName = registerDto.Username.Trim().ToLower(),
            //    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            //    PasswordSalt = hmac.Key
            //};

            //context.Users.Add(user); 
            //await context.SaveChangesAsync();

            //return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) 
        {
            var user = await context.Users
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.UserName.ToLower() == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedPassordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedPassordHash.Length; i++) 
            {
                if (computedPassordHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new UserDto
            {
                Username = loginDto.Username,
                Token = tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMainPhoto)?.Url
            };
        }

        private async Task<bool> UserExists(string userName) 
        {
            return await context.Users.AnyAsync(x => x.UserName.ToLower() == userName.ToLower());
        }
    }
}
