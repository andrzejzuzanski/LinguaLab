using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Core.Entities
{
    public class Word
    {
        public Guid Id { get; set; }
        public string OriginalText { get; set; }
        public string Translation { get; set; }
        public string? PartOfSpeech { get; set; }
        public string? ExampleSentence { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid CreatedById { get; set; }
        public User CreatedBy { get; set; }

    }
}
