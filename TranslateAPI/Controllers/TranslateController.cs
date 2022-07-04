using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranslateAPI.InterFaces;

namespace TranslateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslateController : ControllerBase
    {
        private readonly ITranslate _translate;
        public TranslateController(ITranslate translate) { _translate = translate; }

        //[HttpGet]
        //public async Task<IActionResult> GetUser(int? UserID=null,string? Name = null)
        //    => Ok(await _translate.GetUser(UserID));
    }
}
