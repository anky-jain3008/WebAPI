using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Business.Interface;
using WebAPI.Data.ViewModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder orderBll;

        public OrderController(IOrder orderBll)
        {
            this.orderBll = orderBll;
        }

        [HttpPost]
        public async Task<IActionResult> Create(long customerId, long[] cartIds)
        {
            try
            {
                await orderBll.CreateAsync(customerId, cartIds);
                return Ok(new ResultViewModel<bool> { Success = true });
            }
            catch (CustomException ex)
            {
                return Ok(new ResultViewModel<string> { Success = false, Error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> List(long customerId, int pageIndex, int pagesize)
        {
            try
            {
                return Ok(new ResultViewModel<List<OrderViewModel>> { Success = true, Data = await orderBll.ListAsync(customerId, pageIndex, pagesize) });
            }
            catch (CustomException ex)
            {
                return Ok(new ResultViewModel<string> { Success = false, Error = ex.Message });
            }
        }
    }
}