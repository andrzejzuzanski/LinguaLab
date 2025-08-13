using LinguaLab.Application.Interfaces;
using LinguaLab.Core.Entities;
using LinguaLab.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Infrastructure.Repositories
{
    public class ReviewLogRepository : IReviewLogRepository
    {
        private readonly LinguaLabDbContext _context;

        public ReviewLogRepository(LinguaLabDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ReviewLog log)
        {
            await _context.ReviewLogs.AddAsync(log);
        }
    }
}
