
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class CountryRepository(AplicationContext context) : IGennericRepository<Country>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var country = await context.Countries.FindAsync(id);
            if (country == null) return NotFound();
            context.Countries.Remove(country);
            await Commit();
            return Sucesss();

        }

        public async Task<List<Country>> GetAll() => await context.Countries.AsNoTracking().ToListAsync();


        public async Task<Country> GetById(int id) => await context.Countries.FindAsync(id);

        public async Task<GeneralReponse> Inser(Country item)
        {
            if (!await CheckName(item.Name!, item.Id)) return new GeneralReponse(false, "Data already exists.");
            context.Countries.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(Country item)
        {
            var country = await context.Countries.FindAsync(item.Id);
            if (!await CheckName(item.Name!, item.Id)) return new GeneralReponse(false, "Data already exists.");
            if (country is null) return NotFound();
            country.Name = item.Name;
            await Commit();
            return Sucesss();
        }

        private async Task<bool> CheckName(string name, int id)
        {
            var item = await context.Countries.FirstOrDefaultAsync(item => item.Name!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;
        }
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
