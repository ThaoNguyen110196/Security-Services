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
    public class OvertimeTypeRepository(AplicationContext context) : IGennericRepository<OvertimeType>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
           var item = await context.OvertimeTypes.FindAsync(id);
            if(item is null) return NotFound();
            await Commit();
            return Sucesss();
        }

        public async Task<List<OvertimeType>> GetAll() => await context.OvertimeTypes.AsNoTracking().ToListAsync();


        public async Task<OvertimeType> GetById(int id) => await context.OvertimeTypes.FindAsync(id);
        

        public async Task<GeneralReponse> Inser(OvertimeType item)
        {
           if(!await CheckName(item.Name!)) return NotFound();
           context.OvertimeTypes.Add(item);
            await Commit();
            return Sucesss();

        }

        public async Task<GeneralReponse> Update(OvertimeType item)
        {
            var obj = await context.OvertimeTypes.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (obj is null) return NotFound();
            obj.Name = item.Name;
            await Commit();
            return Sucesss();

        }
        private async Task<bool> CheckName(string name)
        {

            var item = await context.OvertimeTypes.FirstOrDefaultAsync(item => item.Name!.ToLower().Equals(name.ToLower()));
            return item is null;

        }
        public static GeneralReponse NotFound() => new(false, "Sorry deparment not found");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
