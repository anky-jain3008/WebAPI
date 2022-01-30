using System.Threading.Tasks;
using WebAPI.Data.InputModel;

namespace WebAPI.Business.Interface
{
    public interface ICustomer
    {
        Task CreateAsync(CustomerInputModel customerInputModel);
    }
}