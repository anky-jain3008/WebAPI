using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Business.Interface;
using WebAPI.Data.InputModel;
using WebAPI.Data.ViewModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer customerBll;

        public CustomerController(ICustomer customerBll)
        {
            this.customerBll = customerBll;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerInputModel customerInputModel)
        {
            try
            {
                await customerBll.CreateAsync(customerInputModel);
                return Ok(new ResultViewModel<bool> { Success = true });
            }
            catch (CustomException ex)
            {
                return Ok(new ResultViewModel<string> { Success = false, Error = ex.Message });
            }
        }
    }
}