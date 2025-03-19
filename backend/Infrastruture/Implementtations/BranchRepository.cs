using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class BranchRepository(AplicationContext context) : IGennericRepository<Branch>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var dep = await context.Branches.FindAsync(id);
            if (dep is null) return NotFound();

            context.Branches.Remove(dep);
            await Commit();
            return Sucesss();

        }

        public async Task<List<Branch>> GetAll() => await context.Branches.ToListAsync();

        public async Task<Branch> GetById(int id) => await context.Branches.FindAsync(id);

        public async Task<GeneralReponse> Inser(Branch item)
        {
            if (!await CheckName(item.Name!, item.Id)) return new GeneralReponse(false, "Name already exists.");
            if (!await CheckAddress(item.Address!, item.Id)) return new GeneralReponse(false, "Address already exists.");

            context.Branches.Add(item);
            await Commit();
            return Sucesss();
        }

        private async Task<bool> CheckAddress(string name, int id)
        {
            var item = await context.Branches.FirstOrDefaultAsync(item => item.Address!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;
        }

        private async Task<bool> CheckName(string name, int id)
        {
            var item = await context.Branches.FirstOrDefaultAsync(item => item.Name!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;
        }

        public async Task<GeneralReponse> Update(Branch item)
        {
            var up = await context.Branches.FindAsync(item.Id);
            if (up is null) return NotFound();

            if (!await CheckName(item.Name!, item.Id)) return new GeneralReponse(false, "Name already exists.");
            if (!await CheckAddress(item.Address!, item.Id)) return new GeneralReponse(false, "Address already exists.");

            up.Name = item.Name;
            up.Email = item.Email;
            up.Address = item.Address;
            up.ContactNumber = item.ContactNumber;

            await Commit();
            return Sucesss();
        }


        public static GeneralReponse Unique() => new(false, "Data already exists.");
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
