using LinguaLab.Application.Interfaces;
using LinguaLab.Core.Entities;
using LinguaLab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Infrastructure.Repositories
{
    public class AchievementRepository : IAchievementRepository
    {
        private readonly LinguaLabDbContext _context;
        public AchievementRepository(LinguaLabDbContext context) 
        { 
            _context = context; 
        }

        public async Task AddUserAchievementAsync(UserAchievement userAchievement)
        {
            await _context.UserAchievements.AddAsync(userAchievement);
        }

        public async Task<bool> HasAchievementAsync(Guid userId, Guid achievementId)
        {
            return await _context.UserAchievements.AnyAsync(ua => ua.UserId == userId && ua.AchievementId == achievementId);
        }
    }
}
