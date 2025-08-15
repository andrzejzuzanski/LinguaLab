using LinguaLab.Application.Interfaces;
using LinguaLab.Core.Entities;
using LinguaLab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Infrastructure.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly LinguaLabDbContext _context;

        public WordRepository(LinguaLabDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Word word)
        {
            await _context.Words.AddAsync(word);
        }

        public async Task<Word?> GetByIdAsync(Guid wordId)
        {
            return await _context.Words.FindAsync(wordId);
        }

        public async Task<IEnumerable<Word>> GetWordsForUserAsync(Guid userId)
        {
            return await _context.Words
                .Where(w => w.CreatedById == userId)
                .ToListAsync();
        }

        public void Remove(Word word)
        {
            _context.Words.Remove(word);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(Word word)
        {
            _context.Words.Update(word);
        }
    }
}
