using Aplication.Contracts;
using Aplication.DTOS.Employee.DTOs;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class managerRepository(AplicationContext context) : IGennericRepository<Manager>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var item = await context.Managers.FindAsync(id);
            if (item is null) return NotFound();
            context.Managers.Remove(item);
            await Commit();
            return Sucesss();
        }

        public async Task<List<Manager>> GetAll()
        {
            var managers = await context.Managers
                .Include(m => m.Employees)
                .ToListAsync();

            return managers;
        }

        public async Task<Manager> GetById(int id) => await context.Managers.FindAsync(id);

        public async Task<GeneralReponse> Inser(Manager item)
        {
             context.Managers.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(Manager item)
        {
            await Commit();
            return Sucesss();

        }
        public static GeneralReponse NotFound() => new(false, "Sorry manager not found");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
