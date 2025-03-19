using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class TowRepository(AplicationContext context) : IGennericRepository<QuanHuyen>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var dep = await context.Tows.FindAsync(id);
            if(dep is null) return NotFound();
            await Commit();
            return Sucesss();
        }

        public Task<List<QuanHuyen>> GetAll() => context.Tows.ToListAsync();

        

        public async Task<QuanHuyen> GetById(int id) => await context.Tows.FindAsync(id);
        

        public async Task<GeneralReponse> Inser(QuanHuyen item)
        {
            if (!await CheckName(item.Name!)) return new GeneralReponse(false, "Deparment already addted");
            context.Tows.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(QuanHuyen item)
        {
            var dep = await context.Departners.FindAsync(item.Id);
            if (dep is null) return NotFound();
            dep.Name = item.Name;
            await Commit();
            return Sucesss();
        }
        private async Task<bool> CheckName(string name)
        {

            var item = await context.GeneralDepartment.FirstOrDefaultAsync(item => item.Name!.ToLower().Equals(name.ToLower()));
            return item is null;

        }
        public static GeneralReponse NotFound() => new(false, "Sorry deparment not found");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
