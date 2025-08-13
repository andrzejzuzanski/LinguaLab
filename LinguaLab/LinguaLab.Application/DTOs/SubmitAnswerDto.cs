using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.DTOs
{
    public class SubmitAnswerDto
    {
        public Guid WordId { get; set; }
        public int Quality { get; set; }
    }
}
