using LinguaLab.Application.Constants;
using LinguaLab.Application.Interfaces;
using LinguaLab.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.Services
{
    public class AchievementService : IAchievementService
    {
        private readonly IAchievementRepository _achievementRepository;
        private readonly IWordRepository _wordRepository;

        public AchievementService(IAchievementRepository achievementRepository, IWordRepository wordRepository)
        {
            _achievementRepository = achievementRepository;
            _wordRepository = wordRepository;
        }

        public async Task CheckAndAwardAchievementsAsync(User user)
        {
            await TryAwardAchievement(user, AchievementIds.FirstSteps, () => user.TotalXP >= 10);

            var wordCount = (await _wordRepository.GetWordsForUserAsync(user.Id)).Count();
            await TryAwardAchievement(user, AchievementIds.WordSmith, () => wordCount >= 10);

            await TryAwardAchievement(user, AchievementIds.CommittedLearner, () => user.CurrentStreak >= 7);
        }

        private async Task TryAwardAchievement(User user, Guid achievementId, Func<bool> condition)
        {
            if (!await _achievementRepository.HasAchievementAsync(user.Id, achievementId) && condition())
            {
                var newUserAchievement = new UserAchievement
                {
                    UserId = user.Id,
                    AchievementId = achievementId,
                    UnlockedAt = DateTime.UtcNow
                };
                await _achievementRepository.AddUserAchievementAsync(newUserAchievement);
            }
        }
    }
}
