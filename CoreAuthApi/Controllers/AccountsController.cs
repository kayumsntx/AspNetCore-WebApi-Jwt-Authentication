using CoreAuthApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DTOs;

namespace CoreAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(IUserRepository repo) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var resp = await repo.CreateAccount(userDTO);
            return Ok(resp);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var resp = await repo.LoginAccount(loginDTO);
            return Ok(resp);
        }
    }
}
