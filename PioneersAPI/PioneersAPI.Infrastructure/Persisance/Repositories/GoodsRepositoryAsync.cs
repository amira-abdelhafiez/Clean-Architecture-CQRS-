using Microsoft.EntityFrameworkCore;
using PioneersAPI.Application.Interfaces.Repositories;
using PioneersAPI.Domain;
using PioneersAPI.Infrastructure.Persisance.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PioneersAPI.Infrastructure.Persisance.Repositories
{
    internal class GoodsRepositoryAsync : GenericRepositoryAsync<Goods>, IGoodsRepositoryAsync
    {
        private readonly DbSet<Goods> _goods;

        public GoodsRepositoryAsync(ApplicationDBContext dbContext) : base(dbContext)
        {
            _goods = dbContext.Set<Goods>();
        }

        public Task<List<Goods>> SearchGoodsAsync(int GoodId, DateTime? transactionFrom, DateTime? transactionTo, int pageNumber, int pageSize) {
            bool isFromDateValid = transactionFrom != null ? true : false;
            bool isToDateValid = transactionTo != null ? true : false;
            return _goods.Where(e => e.Id == GoodId && (e.TransactionDate >= transactionFrom || isFromDateValid) && (e.TransactionDate <= transactionTo || isToDateValid)).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();
        }
    }
}
