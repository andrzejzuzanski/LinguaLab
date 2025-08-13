using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Core.Entities
{
    public class ReviewLog
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid WordId { get; set; }
        public DateTime ReviewTimestamp { get; set; }
        public int Quality { get; set; }


        public User User { get; set; }
        public Word Word { get; set; }
    }
}
