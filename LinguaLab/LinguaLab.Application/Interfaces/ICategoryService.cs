using LinguaLab.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<CategoryDto?> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto updateCategoryDto);
        Task<bool> DeleteCategoryAsync(Guid categoryId);
    }
}
