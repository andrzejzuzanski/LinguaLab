using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Core.Entities
{
    public class UserAchievement
    {
        public Guid UserId { get; set; }
        public Guid AchievementId { get; set; }
        public DateTime UnlockedAt { get; set; }
        public User User { get; set; }
        public Achievement Achievement { get; set; }
    }
}
