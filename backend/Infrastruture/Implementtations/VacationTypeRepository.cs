
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class VacationTypeRepository(AplicationContext context) : IGennericRepository<VacationType>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var item = await context.VacastionTypes.FindAsync(id);
            if (item is null) return NotFound();
            context.VacastionTypes.Remove(item);
            await Commit();
            return Sucesss();
        }

        public async Task<List<VacationType>> GetAll()  => await context.VacastionTypes.AsNoTracking().ToListAsync();


        public async Task<VacationType> GetById(int id) => await context.VacastionTypes.FindAsync(id);



        public async Task<GeneralReponse> Inser(VacationType item)
        {
            if (!await CheckName(item.Name!, item.Id)) return Unique();
            context.VacastionTypes.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(VacationType item)
        {
            var obj = await context.VacastionTypes.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (obj is null) return NotFound();

            if (!await CheckName(item.Name!, item.Id)) return Unique();

            obj.Name = item.Name;
           
            await Commit();
            return Sucesss();
        }
        private async Task<bool> CheckName(string name, int id)
        {
            var item = await context.VacastionTypes.FirstOrDefaultAsync(item => item.Name!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;
        }

        public static GeneralReponse Unique() => new(false, "Data already exists.");
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
