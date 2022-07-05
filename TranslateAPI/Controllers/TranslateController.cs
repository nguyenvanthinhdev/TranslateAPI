using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranslateAPI.InterFaces;

namespace TranslateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslateController : ControllerBase
    {
        private readonly IUnitOfWork _UOW;
        public TranslateController(IUnitOfWork UOW) { _UOW = UOW; }

        [HttpGet]
        public async Task<IActionResult> Translate([FromQuery]Account account,[FromQuery]TranslateGG translate)
        {
            var res = _UOW.translate.Translate(account, translate);
            await _UOW.SaveAsync();
            return Ok(res);
        }

    }
}
