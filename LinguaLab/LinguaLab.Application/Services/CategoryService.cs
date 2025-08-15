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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description
            };

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task<bool> DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                return false;
            }

            if(!await _categoryRepository.IsEmptyAsync(categoryId))
            {
                throw new InvalidOperationException("Cannot delete a category that contains words.");
            }

            _categoryRepository.Remove(category);
            await _categoryRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (category == null)
            {
                return null;
            }

            category.Name = updateCategoryDto.Name;
            category.Description = updateCategoryDto.Description;

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}
