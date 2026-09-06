using SharedLibrary.DTOs;
using static SharedLibrary.DTOs.ServiceResponse;

namespace CoreAuthApi.Repositories
{
    public interface IUserRepository
    {
        Task<GeneralResponse> CreateAccount(UserDTO userDTO);
         Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    }
}
