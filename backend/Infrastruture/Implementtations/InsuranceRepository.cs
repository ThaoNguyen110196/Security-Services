
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class InsuranceRepository(AplicationContext context) : IGennericRepository<Insurance>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var item = await context.Insurances.FirstOrDefaultAsync(x => x.Id == id);
            if (item is null) return NotFound();
            context.Insurances.Remove(item);
            await Commit();
            return Sucesss();
        }

        public async Task<List<Insurance>> GetAll() => await context.Insurances.AsNoTracking().ToListAsync();


        public async Task<Insurance> GetById(int id)  => await context.Insurances.FindAsync(id);

        public async Task<GeneralReponse> Inser(Insurance item)
        {
            if (!await CheckName(item.InsuranceNumber!, item.Id)) return Unique();
            context.Insurances.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(Insurance item)
        {
            var  insurace = await context.Insurances.FindAsync(item.Id);
            if (insurace is null) return NotFound();

            if (!await CheckName(item.InsuranceNumber!, item.Id)) return Unique();

            insurace.IssueDate = item.IssueDate;
            insurace.InsuranceNumber = item.InsuranceNumber;
            insurace.EmployeeId = item.EmployeeId;
            insurace.IssuePlace = item.IssuePlace;
            insurace.HealthCheckPlace = item.HealthCheckPlace;

            await Commit();
            return Sucesss();
        }

        private async Task<bool> CheckName(string name, int id)
        {
            var item = await context.Insurances.FirstOrDefaultAsync(item => item.InsuranceNumber!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;
        }

        public static GeneralReponse Unique() => new(false, "Data already exists.");
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
