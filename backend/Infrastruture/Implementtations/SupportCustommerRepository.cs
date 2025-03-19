
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class SupportCustommerRepository : IEmployeeSupportRepository
    {
        private readonly AplicationContext _context;

        public SupportCustommerRepository(AplicationContext context)
        {
            _context = context;
        }

        public async Task<GeneralReponse> AddAsync(EmployeeSupport item)
        {
            _context.EmployeeSupports.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> DeleteAsync(int id)
        {
            var obj = await _context.EmployeeSupports.FirstOrDefaultAsync(x => x.Id == id);
            if (obj is null) return NotFound();
            _context.EmployeeSupports.Remove(obj);
            await Commit();
            return Sucesss();
        }

        public async Task<IEnumerable<EmployeeSupport>> GetAllAsync()
        {
            return await _context.EmployeeSupports.ToListAsync();
        }

        public async Task<EmployeeSupport> GetByIdAsync(int id)
        {
            return await _context.EmployeeSupports.FindAsync(id);
        }

        public async Task<GeneralReponse> UpdateAsync(EmployeeSupport item)
        {
            var obj = await _context.EmployeeSupports.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (obj is null) return NotFound();
              obj.ServiceId = item.ServiceId;
            obj.EmployeeId = item.EmployeeId;
            obj.CustomerId = item.CustomerId;
            obj.CashServiceId = item.CashServiceId;
            obj.ServiceId = item.ServiceId;
            obj.IsDeleted = item.IsDeleted;
            await Commit();
            return Sucesss();
        }
        public static GeneralReponse NotFound() => new(false, "Sorry customersuppoet not found");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await _context.SaveChangesAsync();

        public async Task<IEnumerable<EmployeeSupport>> GetCustomerSupportsByCustomerIdAsync(int customerId)
        {
            return await _context.EmployeeSupports
         .Where(es => es.CustomerId == customerId) // Lọc theo CustomerId
         .Include(es => es.CashService)
         .Include(es => es.Customer)
         .Include(es => es.Service)// Bao gồm thông tin CashService
         .ToListAsync(); // Lấy tất cả các bản ghi phù hợp
        }
    }
}
