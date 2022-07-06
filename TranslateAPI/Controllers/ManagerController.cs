using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranslateAPI.InterFaces;

namespace TranslateAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IUnitOfWork _UOW;
        public ManagerController(IUnitOfWork UOW) { _UOW = UOW; }

        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> Get(SystemManager systemManager)
            => Ok(await _UOW.manager.Get(systemManager));
        [HttpPut]
        [Route("UnOrBlock")]
        public async Task<IActionResult> UnOrBlock(UnlockOrBock? UnlockOrBock = null, int? acctive = null)
            => Ok(await _UOW.manager.UnOrBlock(UnlockOrBock, acctive));
    }
}
