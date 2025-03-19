
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class PositionRepository(AplicationContext context) : IGennericRepository<Position>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var item = await context.Positions.FindAsync(id);
            if (item is null) return NotFound();
            context.Positions.Remove(item);
            await Commit();
            return Sucesss();
        }

        public async Task<List<Position>> GetAll() => await context.Positions.AsNoTracking().ToListAsync();

        public async Task<Position> GetById(int id) => await context.Positions.FindAsync(id);

        public async Task<GeneralReponse> Inser(Position item)
        {
            if (!await CheckName(item.Name!, item.Id)) return Unique();
            context.Positions.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(Position item)
        {
            var education = await context.Positions.FindAsync(item.Id);
            if (education is null) return NotFound();
            if (!await CheckName(item.Name!, item.Id)) return Unique();

            education.Name = item.Name;
          
            await Commit();
            return Sucesss();
        }

        private async Task<bool> CheckName(string name, int id)
        {
            var item = await context.Positions.FirstOrDefaultAsync(item => item.Name!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;
        }

        public static GeneralReponse Unique() => new(false, "Data already exists.");
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
