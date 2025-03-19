
using System;
using Aplication.DTOS;
using Aplication.Responses;
using Domain.Entities.Entitie.Account;

namespace Aplication.Contracts
{
    public interface IUserAccount
    {
        Task<GeneralReponse> CreateAsync(Register user);
        Task<LoginResponse> SingInAsync(Login user);
        Task<LoginResponse> RefreshTokenAsync(RefreshToken token);
    }
}
