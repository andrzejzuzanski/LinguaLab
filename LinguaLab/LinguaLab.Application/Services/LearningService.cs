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
    public class LearningService : ILearningService
    {
        private readonly IWordProgressRepository _wordProgressRepository;
        private readonly IReviewLogRepository _reviewLogRepository;
        public LearningService(IWordProgressRepository wordProgressRepository, IReviewLogRepository reviewLogRepository)
        {
            _wordProgressRepository = wordProgressRepository;
            _reviewLogRepository = reviewLogRepository;
        }

        public async Task<IEnumerable<WordDto>> GetWordsForSessionAsync(Guid userId, int sessionSize = 10)
        {
            var wordsForReview = await _wordProgressRepository.GetWordsForReviewAsync(userId, sessionSize);

            var wordsForSession = new List<Word>(wordsForReview);
            var remainingSlots = sessionSize - wordsForSession.Count;

            if (remainingSlots > 0)
            {
                var newWords = await _wordProgressRepository.GetNewWordsForUserAsync(userId, remainingSlots);
                wordsForSession.AddRange(newWords);
            }

            return wordsForSession.Select(w => new WordDto
            {
                Id = w.Id,
                OriginalText = w.OriginalText,
                Translation = w.Translation,
                PartOfSpeech = w.PartOfSpeech,
                ExampleSentence = w.ExampleSentence,
                CategoryId = w.CategoryId
            });
        }

        public async Task ProcessAnswerAsync(Guid userId, SubmitAnswerDto answerDto)
        {
            var reviewLog = new ReviewLog
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                WordId = answerDto.WordId,
                ReviewTimestamp = DateTime.UtcNow,
                Quality = answerDto.Quality,
            };
            await _reviewLogRepository.AddAsync(reviewLog);

            if (answerDto.Quality < 0 || answerDto.Quality > 5)
            {
                throw new ArgumentException("Quality must be between 0 and 5.");
            }

            var progress = await _wordProgressRepository.GetProgressAsync(userId, answerDto.WordId);

            if (progress == null)
            {
                progress = new WordProgress
                {
                    UserId = userId,
                    WordId = answerDto.WordId,
                    NextReviewDate = DateTime.UtcNow
                };
                await _wordProgressRepository.AddProgressAsync(progress);
            }

            if (answerDto.Quality < 3)
            {
                progress.Repetitions = 0;
                progress.Interval = 1;
            }
            else
            {
                progress.Repetitions++;

                progress.EaseFactor = Math.Max(1.3, progress.EaseFactor + 0.1 - (5.0 - answerDto.Quality) * (0.08 + (5.0 - answerDto.Quality) * 0.02));

                if (progress.Repetitions == 1)
                {
                    progress.Interval = 1;
                }
                else if (progress.Repetitions == 2)
                {
                    progress.Interval = 6;
                }
                else
                {
                    progress.Interval = (int)Math.Round(progress.Interval * progress.EaseFactor);
                }
            }

            progress.NextReviewDate = DateTime.UtcNow.AddDays(progress.Interval);
            await _wordProgressRepository.SaveChangesAsync();
        }
    }
}
