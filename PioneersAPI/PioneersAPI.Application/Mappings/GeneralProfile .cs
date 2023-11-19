using AutoMapper;
using PioneersAPI.Application.Features.Goods.Queries.GetAllGoods;
using PioneersAPI.Application.Features.Goods.Queries.SearchGoods;
using PioneersAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PioneersAPI.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Goods, GetAllGoodsViewModel>().ReverseMap();
            CreateMap<Goods, SearchGoodsViewModel>().ReverseMap();
            CreateMap<GetAllGoodsQuery, GetAllGoodsRequestPatameter>();
            CreateMap<SearchGoodsQuery, SearchGoodsRequestParameter>();
        }
    }
}
