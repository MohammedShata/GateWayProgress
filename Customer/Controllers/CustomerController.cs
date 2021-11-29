using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
        public class CustomerController : ControllerBase
        {

            private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        

            [HttpGet]
            public async Task<ActionResult<List<Customer>>> GetAllCustomers()
            {
                return await _customerRepository.GetAllCustomers();
            }
        }
    }
