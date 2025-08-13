using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.DTOs
{
    public class CreateWordDto
    {
        public string OriginalText { get; set; }
        public string Translation { get; set; }
        public Guid CategoryId { get; set; }
        public string? PartOfSpeech { get; set; }
        public string? ExampleSentence { get; set; }
    }
}
