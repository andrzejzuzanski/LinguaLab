using LinguaLab.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.Interfaces
{
    public interface IWordProgressRepository
    {
        Task<List<Word>> GetWordsForReviewAsync(Guid userId, int limit);
        Task<List<Word>> GetNewWordsForUserAsync(Guid userId, int limit);
        Task<WordProgress?> GetProgressAsync(Guid userId, Guid wordId);
        void UpdateProgress(WordProgress progress);
        Task AddProgressAsync(WordProgress progress);
        Task<int> SaveChangesAsync();
    }
}
