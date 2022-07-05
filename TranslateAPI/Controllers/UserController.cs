using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranslateAPI.InterFaces;

namespace TranslateAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _UOW;
        public UserController(IUnitOfWork UOW) { _UOW = UOW; }
        [HttpGet]
        [Route("api/GetUser")]
        public async Task<IActionResult> GetUser(int? UserID = null, string? Name = null)
            => Ok(await _UOW.user.GetUser(UserID, Name));

        [HttpPost]
        [Route("api/CreateUser")]
        public async Task<IActionResult> CreateUser([FromQuery]Account account, [FromQuery]string Password)
        {
            var res = _UOW.user.CreateUser(account, Password);
            await _UOW.SaveAsync();
            return Ok(res);
        }
        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login(Account account)
            => Ok(await _UOW.user.Login(account));

        [HttpPut]
        [Route("api/ChangePass")]
        public async Task<IActionResult> UpdatePassword([FromQuery] Account account, [FromQuery] string Password)
        {
            var res = _UOW.user.ChangerPassword(account, Password);
            await _UOW.SaveAsync();
            return Ok(res);
        }
    }
}
