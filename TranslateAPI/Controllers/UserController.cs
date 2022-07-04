using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranslateAPI.InterFaces;

namespace TranslateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _UserService;
        public UserController(IUser UserService) { _UserService = UserService; }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string Name, string Password_01,string Password_02)
            => Ok(await _UserService.CreateUser(Name, Password_01, Password_02));

        [HttpGet]
        public async Task<IActionResult> GetUser(int? UserID = null, string? Name = null)
            => Ok(await _UserService.GetUser(UserID, Name));
    }
}
