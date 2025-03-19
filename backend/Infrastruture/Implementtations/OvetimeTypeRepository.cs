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
    public class OvetimeTypeRepository(AplicationContext context) : IGennericRepository<OvertimeType>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var item = await context.OvertimeTypes.FindAsync(id);
            if (item is null) return NotFound();
            context.OvertimeTypes.Remove(item);
            await Commit();
            return Sucesss();
        }

        public async Task<List<OvertimeType>> GetAll() => await context.OvertimeTypes.AsNoTracking().ToListAsync();


        public async Task<OvertimeType> GetById(int id) => await context.OvertimeTypes.FindAsync(id);
       

        public async Task<GeneralReponse> Inser(OvertimeType item)
        {
            if (!await CheckName(item.Name!, item.Id)) return Unique();

            context.OvertimeTypes.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(OvertimeType item)
        {
            if (!await CheckName(item.Name!, item.Id)) return Unique();

            context.OvertimeTypes.Update(item);
            await Commit();
            return Sucesss();
        }
        private async Task<bool> CheckName(string name, int id)
        {

            var item = await context.OvertimeTypes.FirstOrDefaultAsync(item => item.Name!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;

        }
        public static GeneralReponse Unique() => new(false, "Data already exists.");
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
