using LinguaLab.Application.Interfaces;
using LinguaLab.Core.Entities;
using LinguaLab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LinguaLab.Infrastructure.Repositories
{
    public class WordProgressRepository : IWordProgressRepository
    {
        private readonly LinguaLabDbContext _context;

        public WordProgressRepository(LinguaLabDbContext context)
        {
            _context = context;
        }

        public async Task AddProgressAsync(WordProgress progress)
        {
            await _context.WordProgresses.AddAsync(progress);
        }

        public async Task<List<Word>> GetNewWordsForUserAsync(Guid userId, int limit)
        {
            return await _context.Words
            .Where(w => w.CreatedById == userId && !_context.WordProgresses.Any(wp => wp.UserId == userId && wp.WordId == w.Id))
            .Take(limit)
            .ToListAsync();
        }

        public async Task<WordProgress?> GetProgressAsync(Guid userId, Guid wordId)
        {
            return await _context.WordProgresses.FindAsync(userId,wordId);
        }

        public async Task<List<Word>> GetWordsForReviewAsync(Guid userId, int limit)
        {
            return await _context.WordProgresses
            .Where(wp => wp.UserId == userId && wp.NextReviewDate <= DateTime.UtcNow)
            .OrderBy(wp => wp.NextReviewDate)
            .Select(wp => wp.Word)
            .Take(limit)
            .ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void UpdateProgress(WordProgress progress)
        {
            _context.WordProgresses.Update(progress);
        }
    }
}
