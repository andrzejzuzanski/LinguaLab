using LinguaLab.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.Interfaces
{
    public interface IWordRepository
    {
        Task<IEnumerable<Word>> GetWordsForUserAsync(Guid userId);
        Task AddAsync(Word word);
        Task<int> SaveChangesAsync();
    }
}
