using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Core.Entities
{
    public class WordProgress
    {
        public Guid UserId { get; set; }
        public Guid WordId { get; set; }

        public double EaseFactor { get; set; } = 2.5;

        public int Interval { get; set; } = 0;

        public int Repetitions { get; set; } = 0;

        public DateTime NextReviewDate { get; set; }

        public User User { get; set; }
        public Word Word { get; set; }
    }
}
