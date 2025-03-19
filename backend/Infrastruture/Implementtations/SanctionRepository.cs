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
    public class SanctionRepository(AplicationContext context) : IGennericRepository<Sanction>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var obj = await context.Sanctions.FirstOrDefaultAsync(x => x.Id == id);
            if (obj is null) return NotFound();
            context.Sanctions.Remove(obj);
            await Commit();
            return Sucesss();
        }

        public async Task<List<Sanction>> GetAll() => await context.Sanctions
            .AsNoTracking()
            .Include(x => x.SanctionType)
            .ToListAsync();
        

        public async Task<Sanction> GetById(int id) => 
             await context.Sanctions.FirstOrDefaultAsync(x => x.EmployeeId == id);
        

        public async Task<GeneralReponse> Inser(Sanction item)
        {
            context.Sanctions .Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(Sanction item)
        {
            var obj = await context.Sanctions.FirstOrDefaultAsync(x=> x.Id == item.Id);
            if (obj is null) return NotFound();
           
            obj.PunishmentDate = item.PunishmentDate;
            obj.Punishment  = item.Punishment;
            obj.SanctionId  = item.SanctionId;
            obj.EmployeeId  = item.EmployeeId;
            obj.Date = item.Date;
            await Commit();
            return Sucesss();
        }
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
