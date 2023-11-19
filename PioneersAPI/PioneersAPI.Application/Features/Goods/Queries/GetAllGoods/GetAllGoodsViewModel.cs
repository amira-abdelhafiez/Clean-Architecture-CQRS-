using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PioneersAPI.Application.Features.Goods.Queries.GetAllGoods
{
    public class GetAllGoodsViewModel
    {
        public string GoodsId { get; set; }
        public string? TransactionId { get; set; }
        public string? TransactionDate { get; set; }
        public string? Amount { get; set; }
        public string? Direction { get; set; }
        public string? Comments { get; set; }
    }
}
