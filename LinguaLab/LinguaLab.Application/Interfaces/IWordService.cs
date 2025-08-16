using LinguaLab.Application.DTOs;
using LinguaLab.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LinguaLab.Application.Interfaces
{
    public interface IWordService
    {
        Task<IEnumerable<WordDto>> GetWordsForUserAsync(Guid userId);
        Task<WordDto> CreateWordAsync(CreateWordDto createWordDto, Guid userId);
        Task<WordDto?> UpdateWordAsync(Guid wordId, UpdateWordDto updateWordDto, Guid userId);
        Task<bool> DeleteWordAsync(Guid wordId, Guid userId);
        Task<int> ImportWordsFromCsvAsync(IFormFile file, Guid userId);
    }
}
