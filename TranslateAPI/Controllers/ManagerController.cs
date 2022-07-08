using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranslateAPI.Entities;
using TranslateAPI.InterFaces;
using TranslateAPIgoogle.Helper;
using HttpRequest = xNet.HttpRequest;
namespace TranslateAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IUnitOfWork _UOW;
        public ManagerController(IUnitOfWork UOW) { _UOW = UOW; }

        [HttpPut]
        [Route("add_coin")]
        public async Task<IActionResult> Add_Coin(string name,int coin)
            => Ok(await _UOW.manager.Add_Coin(name,coin));

        [HttpPut]
        [Route("minus_Coin")]
        public async Task<IActionResult> Minus_Coin(string name, int coin)
            => Ok(await _UOW.manager.Minus_Coin(name, coin));

        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> Get(SystemManager systemManager)
            => Ok(await _UOW.manager.Get(systemManager));
        [HttpPut]
        [Route("UnOrBlock")]
        public async Task<IActionResult> UnOrBlock(UnlockOrBock? UnlockOrBock = null, int? acctive = null)
            => Ok(await _UOW.manager.UnOrBlock(UnlockOrBock, acctive));

        [HttpGet]
        [Route("Get_Infor_Manager")]
        public async Task<IActionResult> Get_Infor_Manager()
            => Ok(await _UOW.manager.Get_Infor_Manager());


        [HttpGet]
        [Route("Get_Order")]
        public  IActionResult Get_Order([FromQuery] int? userID,
                                        [FromQuery] Pagination pagination = null)
        {
            var query = _UOW.manager.Get_Order(userID);
            var addres = PageResult<Translate>.ToPageResult(pagination, query).AsEnumerable();
            pagination.TotalCount = query.Count();
            var res = new PageResult<Translate>(pagination, addres);
            return Ok(res);
        }
    }
}
