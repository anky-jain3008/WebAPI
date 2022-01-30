using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Business.Interface;
using WebAPI.Data.InputModel;
using WebAPI.Data.ViewModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICart cartBll;

        public CartController(ICart cartBll)
        {
            this.cartBll = cartBll;
        }

        [HttpPost]
        public async Task<IActionResult> Add(long customerId, CartInputModel cartInputModel)
        {
            try
            {
                await cartBll.AddAsync(customerId, cartInputModel);
                return Ok(new ResultViewModel<bool> { Success = true });
            }
            catch (CustomException ex)
            {
                return Ok(new ResultViewModel<string> { Success = false, Error = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long customerId, long cartId)
        {
            try
            {
                await cartBll.DeleteAsync(customerId, cartId);
                return Ok(new ResultViewModel<bool> { Success = true });
            }
            catch (CustomException ex)
            {
                return Ok(new ResultViewModel<string> { Success = false, Error = ex.Message });
            }
        }
    }
}