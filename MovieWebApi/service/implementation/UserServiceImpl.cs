using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieWebApi.Persistence.db;
using MovieWebApi.Persistence.entities;
using MovieWebApi.presentation.dto;
using MovieWebApi.service.interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieWebApi.service.implementation
{
    public class UserServiceImpl : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UserServiceImpl(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<bool> RegisterAsync(UserDTO dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return false;

            var userEntity = new UserEntity
            {
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _context.Users.Add(userEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LoginResponseDTO?> LoginAsync(UserDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return null;

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            return new LoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.Id
            };
        }




        public Task<List<UserDTO>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO?> FindByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> CreateUserAsync(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO?> UpdateUserAsync(UserDTO userDTO, long id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO?> DeleteUserAsync(long id)
        {
            throw new NotImplementedException();
        }
    }

}
