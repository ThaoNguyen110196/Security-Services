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
    public class WorkTypeRepository(AplicationContext context) : IGennericRepository<WorkType>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var item = await context.WorkTypes.FindAsync(id);

            if (item is null) return NotFound();
            context.WorkTypes.Remove(item);
            await Commit();
            return Sucesss();
        }

        public async Task<List<WorkType>> GetAll() => await context.WorkTypes.AsNoTracking().ToListAsync();


        public async Task<WorkType> GetById(int id) => await context.WorkTypes.FindAsync(id);


        public async Task<GeneralReponse> Inser(WorkType item)
        {
            if (!await CheckName(item.Name!, item.Id)) return Unique();

            context.WorkTypes.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(WorkType item)
        {
            var workType = await context.WorkTypes.FindAsync(item.Id);
            if (workType is null) return NotFound();

            if (!await CheckName(item.Name!, item.Id)) return Unique();

            workType.Name = item.Name;

            await Commit();
            return Sucesss();
        }

        private async Task<bool> CheckName(string name, int id)
        {
            var item = await context.WorkTypes.FirstOrDefaultAsync(item => item.Name!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;
        }

        public static GeneralReponse Unique() => new(false, "Data already exists.");
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
