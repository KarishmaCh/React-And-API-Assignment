using System.Linq;
using Customer.Model;
using Customer.ViewModels;
using Microsoft.EntityFrameworkCore;
using static Customer.service.Customerservice;

namespace Customer.service
{
    public interface ICustomerservice
    {
        Task<List<CustomerModel>> GetCustomersAsync();
    }
    public class Customerservice : ICustomerservice
    {
        private readonly AppDbContext _context;


       
        public Customerservice(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<CustomerModel>> GetCustomersAsync()
        {
            return await _context.Customers.Include(x=>x.Location).ToListAsync();
        }



        public async Task<CustomerModel> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.Location)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int> CreateCustomerAsync(CustomerDTO customer)
        {
            CustomerModel cust = new CustomerModel()
            {
                Email = customer.Email,
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber,
                Id = customer.Id,
                
            };
            _context.Customers.Add(cust);
            await _context.SaveChangesAsync();
            if (customer.location != null)
            {
                Location location = new Location()
                {
                    Address = customer.location.Address,
                    City = customer.location.City,
                    CustomerId = cust.Id,
                    State = customer.location.State,
                    Customer = cust

                };
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            }

            return cust.Id;
        }

        public async Task UpdateCustomerAsync(int id, UpdateCustomerDto updatedCustomer)
        {
            var existingCustomer = await _context.Customers.FindAsync(id);

            if (existingCustomer == null)
            {
                throw new ArgumentException("Customer not found.");
            }

            existingCustomer.Name = updatedCustomer.Name;
            existingCustomer.Email = updatedCustomer.Email;
            existingCustomer.PhoneNumber = updatedCustomer.PhoneNumber;
           
            Location location = null;
            if (existingCustomer.Location != null)
            {
                location = existingCustomer.Location;
                location.Address = updatedCustomer.Address;
                location.City = updatedCustomer.City;
                location.State = updatedCustomer.State;
            }

            await _context.SaveChangesAsync();
        }
            public async Task<int> DeleteCustomerAsync(int id)
        {
            var location = await _context.Locations.FirstOrDefaultAsync(x=>x.CustomerId==id);
            if (location == null)
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                   return await _context.SaveChangesAsync();
                }
            }
            return 0;
        }
    }
}
