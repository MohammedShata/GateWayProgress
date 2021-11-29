using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> customers = new List<Customer>();
        public CustomerRepository()
        {
            customers.Add(new Customer()
            {
                Id = 0,
                FirstName = "mohamed",
                LastName = "ahmed",
                EmailAddress = "mohamedahmed@yahoo.com"
            });

            customers.Add(new Customer()
            {
                Id = 1,
                FirstName = "ahmed",
                LastName = "mohamed",
                EmailAddress = "ahmedmohamed@yahoo.com"
            });
        }
        public Task<List<Customer>> GetAllCustomers()
        {
            return Task.FromResult(customers);
        }
    }
}


