using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Business.Interface;
using WebAPI.Data.Model;
using WebAPI.Data.ViewModel;

namespace WebAPI.Business
{
    public class OrderBll : IOrder
    {
        private readonly APIDbContect db;

        public OrderBll(APIDbContect db)
        {
            this.db = db;
        }

        public async Task CreateAsync(long customerId, long[] cartIds)
        {
            Customer customer = await db.Set<Customer>().FirstOrDefaultAsync(x => x.Id == customerId);
            if (customer == null)
            {
                throw new CustomException(CommonUtil.InvalidCustomer);
            }

            List<Cart> carts = await db.Set<Cart>().Include(x => x.Product).Where(x => x.CustomerId == customerId && cartIds.Contains(x.Id) && x.Count > 0).ToListAsync();
            if (carts.Count > 0)
            {
                Order order = new Order
                {
                    CustomerId = customerId,
                    PlacedOn = DateTime.UtcNow,
                    Status = "Order Placed"
                };

                List<OrderDetail> orderDetails = carts.Select(x => new OrderDetail
                {
                    Order = order,
                    ProductId = x.ProductId,
                    Count = x.Count,
                    AmountPerItem = x.Product.Amount,
                    TotalAmount = x.Product.Amount * x.Count
                }).ToList();

                order.TotalAmount = orderDetails.Sum(x => x.TotalAmount);

                carts.ForEach(x => x.Count = 0);

                _ = await db.Set<Order>().AddAsync(order);
                await db.Set<OrderDetail>().AddRangeAsync(orderDetails);
                _ = await db.SaveChangesAsync();
            }
            else
            {
                throw new CustomException(CommonUtil.EmptyCart);
            }
        }

        public async Task<List<OrderViewModel>> ListAsync(long customerId, int pageIndex, int pagesize)
        {
            Customer customer = await db.Set<Customer>().FirstOrDefaultAsync(x => x.Id == customerId);
            if (customer == null)
            {
                throw new CustomException(CommonUtil.InvalidCustomer);
            }

            return await db.Set<Order>()
                .Where(x => x.CustomerId == customerId)
                .OrderByDescending(x => x.Id)
                .Skip(pageIndex * pagesize)
                .Take(pagesize)
                .Select(x => new OrderViewModel
                {
                    Id = x.Id,
                    PlacedOn = x.PlacedOn,
                    Status = x.Status,
                    TotalAmount = x.TotalAmount
                }).ToListAsync();
        }
    }
}