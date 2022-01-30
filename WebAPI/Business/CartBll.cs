using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAPI.Business.Interface;
using WebAPI.Data.InputModel;
using WebAPI.Data.Model;
using WebAPI.Data.ViewModel;

namespace WebAPI.Business
{
    public class CartBll : ICart
    {
        private readonly APIDbContect db;

        public CartBll(APIDbContect db)
        {
            this.db = db;
        }

        public async Task AddAsync(long customerId, CartInputModel cartInputModel)
        {
            Customer customer = await db.Set<Customer>().FirstOrDefaultAsync(x => x.Id == customerId);
            if (customer == null)
            {
                throw new CustomException(CommonUtil.InvalidCustomer);
            }

            Cart cart = await db.Set<Cart>().FirstOrDefaultAsync(x => x.CustomerId == customerId && x.ProductId == cartInputModel.ProductId);
            if (cart != null)
            {
                cart.Count = cartInputModel.Count;
            }
            else
            {
                cart = new Cart
                {
                    CustomerId = customerId,
                    ProductId = cartInputModel.ProductId,
                    Count = cartInputModel.Count
                };

                await db.Set<Cart>().AddAsync(cart);
                _ = await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(long customerId, long cartId)
        {
            Customer customer = await db.Set<Customer>().FirstOrDefaultAsync(x => x.Id == customerId);
            if (customer == null)
            {
                throw new CustomException(CommonUtil.InvalidCustomer);
            }

            Cart _cart = await db.Set<Cart>().FirstOrDefaultAsync(x => x.Id == cartId && x.CustomerId == customerId);
            if (_cart != null)
            {
                _cart.Count = 0;
                _ = await db.SaveChangesAsync();
            }
        }
    }
}