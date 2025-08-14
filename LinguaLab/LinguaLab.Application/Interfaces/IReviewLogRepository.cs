using LinguaLab.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.Interfaces
{
    public interface IReviewLogRepository
    {
        Task AddAsync(ReviewLog log);
        Task<DateTime?> GetLastReviewTimestampForUserAsync(Guid userId);
    }
}
