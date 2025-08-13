using LinguaLab.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaLab.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterUserAsync(RegisterUserDto registerUserDto);
        Task<LoginResponseDto?> LoginUserAsync(LoginUserDto loginUserDto);
    }
}
