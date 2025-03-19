
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class ServiceRepositorys: IServiceRepository
    {
        private readonly AplicationContext _context;

        public ServiceRepositorys(AplicationContext context)
        {
            _context = context;
        }

        public async Task<Service> GetByIdAsync(int id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<GeneralReponse> AddAsync(Service item)

        {
            if (!await CheckName(item.ServiceName!, item.Id)) return Unique();
            _context.Services.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> UpdateAsync(Service item)
        {
            var obj = await _context.Services.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (obj is null) return NotFound();

            if (!await CheckName(item.ServiceName!, item.Id)) return Unique();

            obj.ServiceName = item.ServiceName;
            obj.Description = item.Description;
            obj.Price = item.Price;
            obj.Status = item.Status;
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> DeleteAsync(int id)
        {
            var obj = await _context.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (obj is null) return NotFound();
            _context.Services.Remove(obj);
            await Commit();
            return Sucesss();
        }

        private async Task<bool> CheckName(string name, int id)
        {
            var item = await _context.Services.FirstOrDefaultAsync(item => item.ServiceName!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;
        }

        public static GeneralReponse Unique() => new(false, "Data already exists.");
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await _context.SaveChangesAsync();
    }
}
