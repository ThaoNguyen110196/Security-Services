using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class SanctionTypeRepository(AplicationContext context) : IGennericRepository<SanctionType>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var item = await context.SanctionTypes.FindAsync(id);
            if (item is null) return NotFound();
            context.SanctionTypes.Remove(item);
            await Commit();
            return Sucesss();

        }

        public async Task<List<SanctionType>> GetAll() => await context.SanctionTypes.AsNoTracking().ToListAsync();


        public async Task<SanctionType> GetById(int id) => await context.SanctionTypes.FindAsync(id);

        public async Task<GeneralReponse> Inser(SanctionType item)
        {
            if (!await CheckName(item.Name!, item.Id)) return Unique();
            context.SanctionTypes.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(SanctionType item)
        {
            if (!await CheckName(item.Name!, item.Id)) return Unique();
            context.SanctionTypes.Update(item);
            await Commit();
            return Sucesss();
        }
        private async Task<bool> CheckName(string name, int id)
        {
            var item = await context.SanctionTypes.FirstOrDefaultAsync(item => item.Name!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;
        }


        public static GeneralReponse Unique() => new(false, "Data already exists.");
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
