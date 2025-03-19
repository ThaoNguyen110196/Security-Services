
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class EducationRepository(AplicationContext context) : IGennericRepository<Education>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var item = await context.Educations.FindAsync(id);
            if (item is null) return NotFound();
            context.Educations.Remove(item);
            await Commit();
            return Sucesss();
        }

        public async Task<List<Education>> GetAll() => await context.Educations.AsNoTracking().ToListAsync();

        public async Task<Education> GetById(int id) => await context.Educations.FindAsync(id);

        public async Task<GeneralReponse> Inser(Education item)
        {
            if (!await CheckName(item.Name!, item.Id)) return Unique();
            context.Educations.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(Education item)
        {
            var education = await context.Educations.FindAsync(item.Id);
            if (education is null) return NotFound();

            if (!await CheckName(item.Name!, item.Id)) return Unique();

            education.Name = item.Name;
          
            await Commit();
            return Sucesss();
        }
        private async Task<bool> CheckName(string name, int id)
        {
            var item = await context.Educations.FirstOrDefaultAsync(item => item.Name!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;
        }

        public static GeneralReponse Unique() => new(false, "Data already exists.");
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");
        private async Task Commit() => await context.SaveChangesAsync();
    }
}
