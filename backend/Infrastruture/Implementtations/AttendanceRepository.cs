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
    public class AttendanceRepository(AplicationContext context) : IGennericRepository<Attendance>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var item = await context.Attendances.FindAsync(id);
            if (item is null) return NotFound();
            context.Attendances.Remove(item);
            await Commit();
            return Sucesss();
        }

        public async Task<List<Attendance>> GetAll() => await context.Attendances.AsNoTracking().ToListAsync();


        public async Task<Attendance> GetById(int id) => await context.Attendances.FindAsync(id);
        

        public async Task<GeneralReponse> Inser(Attendance item)
        {
            context.Attendances.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(Attendance item)
        {
            var attdance = await context.Attendances.FindAsync(item.Id);
            if (attdance is null) return NotFound();
            attdance.HoursOut = item.HoursOut;

            await Commit();
            return Sucesss();
        }
        public static GeneralReponse NotFound() => new(false, "Sorry deparment not found");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
