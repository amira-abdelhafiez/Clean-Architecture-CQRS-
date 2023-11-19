using System;
using PioneersAPI.Domain;

namespace PioneersAPI.Application.Interfaces.Repositories
{
    public interface IGoodsRepositoryAsync : IGenericRepositoryAsync<Goods>
    {
        Task<List<Goods>> SearchGoodsAsync(int GoodId, DateTime? transactionFrom, DateTime? transactionTo, int pageNumber, int pageSize);
    }
}
