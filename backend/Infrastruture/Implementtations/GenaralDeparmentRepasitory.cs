
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastruture.Implementtations
{
    public class GenaralDeparmentRepasitory(AplicationContext context) : IGennericRepository<GeneralDepartment>
    {
        public  async Task<GeneralReponse> Delete(int id)
        {
            var dep = await context.GeneralDepartment.FindAsync(id);
            if(dep is null) return NotFound();
            context.GeneralDepartment.Remove(dep);
            await Commit();
            return Sucesss();
        }

        public  async Task<List<GeneralDepartment>> GetAll() => await context.GeneralDepartment.ToListAsync();

        public async Task<GeneralDepartment> GetById(int id) => await context.GeneralDepartment.FirstAsync(_=> _.Id == id);

        public async Task<GeneralReponse> Inser(GeneralDepartment item)
        {
            if (!await CheckName(item.Name!, item.Id)) return Unique();
            context.GeneralDepartment.Add(item);    
            await Commit();
            return Sucesss();
        }

        private async Task<bool> CheckName(string name, int id)
        {
            var item = await context.GeneralDepartment.FirstOrDefaultAsync(item => item.Name!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;
        }

        public async Task<GeneralReponse> Update(GeneralDepartment item)
        {
            var dep = await context.GeneralDepartment.FindAsync(item.Id);
            if (dep is null) return NotFound();

            if (!await CheckName(item.Name!, item.Id)) return Unique();

            dep.Name = item.Name;

            await Commit();
            return Sucesss();
        }

        public static GeneralReponse Unique() => new(false, "Data already exists.");
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();

        
    }


}
