using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Timezone { get; set; } = "UTC";
        public int CurrentLevel { get; set; } = 1;
        public int TotalXP { get; set; } = 0;
        public int CurrentStreak { get; set; } = 0;
    }
}
