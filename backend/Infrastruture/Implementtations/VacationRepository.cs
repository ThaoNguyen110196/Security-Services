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
    public class VacationRepository(AplicationContext context) : IGennericRepository<Vacation>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var obj = await context.Vacations.FirstOrDefaultAsync(x => x.Id == id);
            if (obj is null) return NotFound();
            context.Vacations.Remove(obj);
            await Commit();
            return Sucesss();
        }

        public async Task<List<Vacation>> GetAll() => await context.Vacations
             .AsNoTracking()
             .Include(t => t.VacationType)
             .ToListAsync();


        public async Task<Vacation> GetById(int id) => await context.Vacations
             .FirstOrDefaultAsync(x => x.EmployeeId == id);



        public async Task<GeneralReponse> Inser(Vacation item)
        {
            context.Vacations.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(Vacation item)
        {
            var obj = await context.Vacations.FirstOrDefaultAsync(x=>x.Id == item.Id);
            if(obj is null) return NotFound();
            obj.StartDtae = item.StartDtae;
            obj.NumberOfDays = item.NumberOfDays;
            obj.VacationTypeId = item.VacationTypeId;
            await Commit();
            return Sucesss();



        }
        public static GeneralReponse NotFound() => new(false, "Sorry vacations not found");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
