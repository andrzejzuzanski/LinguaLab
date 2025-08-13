using LinguaLab.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.Interfaces
{
    public interface ILearningService
    {
        Task<IEnumerable<WordDto>> GetWordsForSessionAsync(Guid userId, int sessionSize = 10);
        Task ProcessAnswerAsync(Guid userId, SubmitAnswerDto answerDto);
    }
}
