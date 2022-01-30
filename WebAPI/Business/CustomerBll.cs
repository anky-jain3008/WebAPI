using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebAPI.Business.Interface;
using WebAPI.Data.InputModel;
using WebAPI.Data.Model;
using WebAPI.Data.ViewModel;

namespace WebAPI.Business
{
    public class CustomerBll : ICustomer
    {
        private readonly APIDbContect db;

        public CustomerBll(APIDbContect db)
        {
            this.db = db;
        }

        public async Task CreateAsync(CustomerInputModel customerInputModel)
        {
            customerInputModel.Email = customerInputModel.Email.Trim().ToLower();

            Customer customer = await db.Set<Customer>().FirstOrDefaultAsync(x => x.Email == customerInputModel.Email);
            if (customer != null)
            {
                throw new CustomException(CommonUtil.CustomerExist);
            }

            customer = new Customer
            {
                FirstName = customerInputModel.FirstName.Trim(),
                LastName = customerInputModel.LastName.Trim(),
                Email = customerInputModel.Email
            };
            _ = await db.Set<Customer>().AddAsync(customer);
            _ = db.SaveChangesAsync();
        }
    }
}