using Microsoft.AspNetCore.Mvc;
using PioneersAPI.Application.Features.Goods.Queries.GetAllGoods;
using PioneersAPI.Application.Features.Goods.Queries.SearchGoods;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PioneersAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GoodsController : BaseAPIController
    {
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllGoodsRequestPatameter filter)
        {
            return Ok(await Mediator.Send(new GetAllGoodsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet(Name = "Search")]
        public async Task<IActionResult> SearchGoodsByIdAndDate([FromQuery] SearchGoodsRequestParameter filter)
        {
            return Ok(await Mediator.Send(new SearchGoodsQuery() { GoodsId = filter.GoodsId, DateFrom = filter.DateFrom, DateTo = filter.DateTo, PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }
    }
}
