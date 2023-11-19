using PioneersAPI.Application.Wrappers;

namespace PioneersAPI.Application.Features.Goods.Queries.SearchGoods
{
    public class SearchGoodsRequestParameter : RequestParameter
    {
        public int GoodsId { get; set; }
        public DateTime DateFrom {get;set; }
        public DateTime DateTo { get;set; }

    }
}
