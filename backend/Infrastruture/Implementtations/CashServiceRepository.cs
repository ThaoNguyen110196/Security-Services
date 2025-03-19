using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Service;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class CashServiceRepository: ICashServiceRepository
    {
        private readonly AplicationContext _context;

        public CashServiceRepository(AplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CashService>> GetAllCashServicesAsync()
        {
            return await _context.CashServices.ToListAsync();
        }

        public async Task<CashService> GetCashServiceByIdAsync(int id)
        {
            return await _context.CashServices.FindAsync(id);
        }

        public async Task<GeneralReponse> AddCashServiceAsync(CashService item)
        {
            if (!await CheckName(item.Name!, item.Id)) return Unique();
            _context.CashServices.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> UpdateCashServiceAsync(CashService item)
        {
            var obj = await _context.CashServices.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (obj is null) return NotFound();

            if (!await CheckName(item.Name!, item.Id)) return Unique();

            obj.ServiceType = item.ServiceType;
            obj.Conditions = item.Conditions;
            obj.EmployeeSupports = item.EmployeeSupports;
            obj.Name = item.Name;
            obj.Scope = item.Scope;
            obj.Price = item.Price;

            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> DeleteCashServiceAsync(int id)
        {
            var obj = await _context.CashServices.FirstOrDefaultAsync(x => x.Id == id);
            if (obj is null) return NotFound();
            _context.CashServices.Remove(obj);
            await Commit();
            return Sucesss();
        }

        private async Task<bool> CheckName(string name, int id)
        {
            var item = await _context.CashServices.FirstOrDefaultAsync(item => item.Name!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;
        }

        public static GeneralReponse Unique() => new(false, "Data already exists.");
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await _context.SaveChangesAsync();
    }
}
