using LinguaLab.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.Interfaces
{
    public interface IAchievementRepository
    {
        Task<bool> HasAchievementAsync(Guid userId, Guid achievementId);
        Task AddUserAchievementAsync(UserAchievement userAchievement);
    }
}
