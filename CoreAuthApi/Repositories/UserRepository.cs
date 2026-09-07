using CoreAuthApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static SharedLibrary.DTOs.ServiceResponse;

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

        public async Task<ServiceResponse.GeneralResponse> CreateAccount(UserDTO userDTO)
        {
            if (userDTO is null) return new GeneralResponse(false, "User data is null.");
            var newUser = new ApplicationUser
            {
                Name = userDTO.Name,
                PasswordHash = userDTO.Password,
                Email = userDTO.Email,
                UserName = userDTO.Email
            };
            var user = await _userManager.FindByEmailAsync(newUser.Email);
            if (user != null)
                return new GeneralResponse(false, "User already exists.");

            var createUser = await _userManager.CreateAsync(newUser, userDTO.Password);
            if (!createUser.Succeeded) return new GeneralResponse(false, "User Creation failed.");

            var checkAdmin = await _roleManager.RoleExistsAsync("Admin");
            if (!checkAdmin)
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await _userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResponse(true, "Account created successfully.");
            }
            else
            {
                var checkUser = await _roleManager.RoleExistsAsync("User");
                if (!checkUser)

                    await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });


                await _userManager.AddToRoleAsync(newUser, "User");
                return new GeneralResponse(true, "Account created successfully.");
            }
        }

        public async Task<ServiceResponse.LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            if (loginDTO is null)
                return new LoginResponse(false, "Login data is null.", null!);
            var getUser = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new LoginResponse(false, "User not Found.", null!);
            bool checkPassword = await _userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkPassword)
                return new LoginResponse(false, "Invalid username/password", null!);
            var getUserRole = await _userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole.First());
            var token = GenerateToken(userSession);
            return new LoginResponse(true, "Login successful.", token);

        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[] {

            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"]!,
                audience: _config["Jwt:Audience"]!,
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
