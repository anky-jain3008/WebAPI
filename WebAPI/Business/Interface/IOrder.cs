using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Data.ViewModel;

namespace WebAPI.Business.Interface
{
    public interface IOrder
    {
        Task CreateAsync(long customerId, long[] cartIds);
        Task<List<OrderViewModel>> ListAsync(long customerId, int pageIndex, int pagesize);
    }
}