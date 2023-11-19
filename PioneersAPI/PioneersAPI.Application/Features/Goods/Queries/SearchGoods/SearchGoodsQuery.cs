using AutoMapper;
using MediatR;
using PioneersAPI.Application.Interfaces.Repositories;
using PioneersAPI.Application.Wrappers;

namespace PioneersAPI.Application.Features.Goods.Queries.SearchGoods
{
    public class SearchGoodsQuery : IRequest<Response<IEnumerable<SearchGoodsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int GoodsId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
    public class SearchGoodsQueryHandler : IRequestHandler<SearchGoodsQuery, Response<IEnumerable<SearchGoodsViewModel>>>
    {
        private readonly IGoodsRepositoryAsync _goodsRepository;
        private readonly IMapper _mapper;
        public SearchGoodsQueryHandler(IGoodsRepositoryAsync goodsRepository, IMapper mapper)
        {
            _goodsRepository = goodsRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<SearchGoodsViewModel>>> Handle(SearchGoodsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<SearchGoodsRequestParameter>(request);
            var goods = await _goodsRepository.SearchGoodsAsync(validFilter.GoodsId, validFilter.DateFrom, validFilter.DateTo, validFilter.PageNumber, validFilter.PageSize);
            var goodsViewModel = _mapper.Map<IEnumerable<SearchGoodsViewModel>>(goods);
            return new Response<IEnumerable<SearchGoodsViewModel>>(goodsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
