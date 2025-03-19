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
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;

namespace Infrastruture.Implementtations
{
    public class OvertimeRepository(AplicationContext context) : IGennericRepository<OverTime>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var ovetime = await context.OverTime.FirstOrDefaultAsync(o => o.Id == id);
            if(ovetime is null) return NotFound();
            context.OverTime.Remove(ovetime);
            await Commit();
            return Sucesss();
        }

        public async Task<List<OverTime>> GetAll() => await context.OverTime
            .AsNoTracking()
            .Include(over => over.OvertimeType)
            .ToListAsync();


        public async Task<OverTime> GetById(int id) => await context.OverTime
            .FirstOrDefaultAsync(over => over.EmployeeId == id);
        

        public async Task<GeneralReponse> Inser(OverTime item)
        {
            context.OverTime.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(OverTime item)
        {
            var obj = await context.OverTime.FirstOrDefaultAsync(over => over.Id == item.Id);
            if(obj is null) return NotFound();

            obj.StartDate = item.StartDate;
            obj.EndDate = item.EndDate;
            obj.OvertimTypeId = item.OvertimTypeId;
            obj.EmployeeId = item.EmployeeId;
            obj.ApprovedById = item.ApprovedById;
            obj.ApprovalDate = item.ApprovalDate;
            obj.Remarks = item.Remarks;

            context.OverTime.Update(obj);
            await Commit();
            return Sucesss();

        }
       
        public static GeneralReponse NotFound() => new(false, "Sorry deparment not found");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
