using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Account;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class CustomerRepository :ICustomerRepository
    {
        private readonly AplicationContext _context;

        public CustomerRepository(AplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            // Check if a customer with the same email already exists
            var existingCustomer = await FindCustomerByEmail(customer.Email);
            if (existingCustomer != null)
            {
            
                throw new InvalidOperationException("A customer with this email already exists.");
            }

            // Add the new customer to the context
            _context.Customers.Add(customer);

            // Save changes to the database
            await Commit();

            // Return the newly added customer
            return customer;
        }

        public async Task<GeneralReponse> UpdateCustomerAsync(Customer item)
        {

            var obj = await _context.Customers.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (obj is null) return NotFound();
            
             obj.Name = item.Name;

            _context.Customers.Update(obj);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> DeleteCustomerAsync(int id)
        {
            // Xóa tất cả các mục trong CustomerSupport có CustomerId tương ứng
            var customerSupports = _context.EmployeeSupports.Where(cs => cs.CustomerId == id);
            _context.EmployeeSupports.RemoveRange(customerSupports);

            // Xóa Customer
            var customer = await _context.Customers.FindAsync(id);
            if (customer is null) return NotFound();
            _context.Customers.Remove(customer);
            await Commit();
            return Sucesss();

        }
        private async Task<AplictionUser> FindCustomerByEmail(string email) =>
           await _context.AplictionUsers.FirstOrDefaultAsync(user => user.Email!.ToLower()!.Equals(email!.ToLower()));
        public static GeneralReponse NotFound() => new(false, "Sorry customer not found");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await _context.SaveChangesAsync();
    }

}

