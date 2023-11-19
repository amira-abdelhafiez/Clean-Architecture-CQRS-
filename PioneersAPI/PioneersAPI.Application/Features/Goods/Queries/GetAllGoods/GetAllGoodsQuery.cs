using AutoMapper;
using MediatR;
using PioneersAPI.Application.Interfaces.Repositories;
using PioneersAPI.Application.Wrappers;

namespace PioneersAPI.Application.Features.Goods.Queries.GetAllGoods
{
    public class GetAllGoodsQuery : IRequest<Response<IEnumerable<GetAllGoodsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllGoodsQuery, Response<IEnumerable<GetAllGoodsViewModel>>>
    {
        private readonly IGoodsRepositoryAsync _goodsRepository;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IGoodsRepositoryAsync goodsRepository, IMapper mapper)
        {
            _goodsRepository = goodsRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetAllGoodsViewModel>>> Handle(GetAllGoodsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllGoodsRequestPatameter>(request);
            var goods = await _goodsRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var goodsViewModel = _mapper.Map<IEnumerable<GetAllGoodsViewModel>>(goods);
            return new Response<IEnumerable<GetAllGoodsViewModel>>(goodsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
