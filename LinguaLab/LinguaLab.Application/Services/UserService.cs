using LinguaLab.Application.DTOs;
using LinguaLab.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserProfileDto?> GetUserProfileAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            return new UserProfileDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                CurrentLevel = user.CurrentLevel,
                TotalXP = user.TotalXP,
                CurrentStreak = user.CurrentStreak,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
