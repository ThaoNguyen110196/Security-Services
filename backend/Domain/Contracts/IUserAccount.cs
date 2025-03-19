

using Microsoft.Win32;

namespace Domain.Contracts
{
     public interface IUserAccount
    {

        Task<GeneralRespose> CreateAsync(Register user);
        Task<GeneralRespose> SignInAsync(Login user);

    }
}
