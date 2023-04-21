using Customer.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Customer;
using Customer.Model;
using Customer.ViewModels;

namespace Customer.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly Customerservice _service;

        public CustomersController(Customerservice service)
        {
            _service = service;
        }

       [HttpGet]
public async Task<ActionResult<List<CustomerModel>>> GetCustomers()
{
    var customers = await _service.GetCustomersAsync();
    return customers;
}

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>> GetCustomerById(int id)
        {
            var customer = await _service.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateCustomer(CustomerDTO customer)
        {
            var id = await _service.CreateCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { Id = id }, id);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerDto updatedCustomer)
        {
            await _service.UpdateCustomerAsync(id, updatedCustomer);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<int> DeleteCustomer(int id)
        {
            return await _service.DeleteCustomerAsync(id);
        }
    }
}



