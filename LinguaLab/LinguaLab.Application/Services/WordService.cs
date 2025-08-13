using LinguaLab.Application.DTOs;
using LinguaLab.Application.Interfaces;
using LinguaLab.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.Services
{
    public class WordService : IWordService
    {
        private readonly IWordRepository _wordRepository;

        public WordService(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public async Task<WordDto> CreateWordAsync(CreateWordDto createWordDto, Guid userId)
        {
            var word = new Word
            {
                Id = Guid.NewGuid(),
                OriginalText = createWordDto.OriginalText,
                Translation = createWordDto.Translation,
                PartOfSpeech = createWordDto.PartOfSpeech,
                ExampleSentence = createWordDto.ExampleSentence,
                CategoryId = createWordDto.CategoryId,
                CreatedById = userId
            };

            await _wordRepository.AddAsync(word);
            await _wordRepository.SaveChangesAsync();

            return new WordDto
            {
                Id = word.Id,
                OriginalText = word.OriginalText,
                Translation = word.Translation,
                PartOfSpeech = word.PartOfSpeech,
                ExampleSentence = word.ExampleSentence,
                CategoryId = word.CategoryId
            };
        }

        public async Task<IEnumerable<WordDto>> GetWordsForUserAsync(Guid userId)
        {
            var words = await _wordRepository.GetWordsForUserAsync(userId);

            return words.Select(w => new WordDto
            {
                Id = w.Id,
                OriginalText = w.OriginalText,
                Translation = w.Translation,
                PartOfSpeech = w.PartOfSpeech,
                ExampleSentence = w.ExampleSentence,
                CategoryId = w.CategoryId
            });
        }
    }
}
