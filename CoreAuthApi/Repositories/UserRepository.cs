using CoreAuthApi.Data;
using Microsoft.AspNetCore.Identity;
using SharedLibrary.DTOs;

namespace CoreAuthApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
     private readonly IConfiguration _config;

        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }

        public Task<ServiceResponse.GeneralResponse> CreateAccount(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse.LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }
    }
}
