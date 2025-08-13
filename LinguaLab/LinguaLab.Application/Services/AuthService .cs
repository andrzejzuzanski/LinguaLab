using LinguaLab.Application.DTOs;
using LinguaLab.Application.Interfaces;
using LinguaLab.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto?> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginUserDto.Email);
            if (user == null) return null;

            if(!BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.PasswordHash))
            {
                return null;
            }

            var token = _tokenService.CreateToken(user);
            return new LoginResponseDto { Token = token };
        }

        public async Task<bool> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            var existingUser = await _userRepository.UserExistsByEmailAsync(registerUserDto.Email);

            if (existingUser)
            {
                return false;
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = registerUserDto.Email,
                Username = registerUserDto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password),
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }
    }
}
