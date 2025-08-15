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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LinguaLabDbContext _context;

        public CategoryRepository(LinguaLabDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<bool> IsEmptyAsync(Guid categoryId)
        {
            return !await _context.Words
                .AnyAsync(w => w.CategoryId == categoryId);
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
