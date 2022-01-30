using System.Threading.Tasks;
using WebAPI.Data.InputModel;

namespace WebAPI.Business.Interface
{
    public interface ICart
    {
        Task AddAsync(long customerId, CartInputModel cartInputModel);
        Task DeleteAsync(long customerId, long cartId);
    }
}