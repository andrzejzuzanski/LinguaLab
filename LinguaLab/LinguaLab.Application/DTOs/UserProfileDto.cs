using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.DTOs
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int CurrentLevel { get; set; }
        public int TotalXP { get; set; }
        public int CurrentStreak { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
