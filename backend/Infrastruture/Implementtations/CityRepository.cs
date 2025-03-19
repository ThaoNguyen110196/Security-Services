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
    public class CityRepository(AplicationContext context) : IGennericRepository<City>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var dep = await context.Citys.FindAsync(id);
            if(dep is null ) return NotFound();
            context.Citys.Remove(dep);  
            await Commit();
            return Sucesss();

        }

        public async Task<List<City>> GetAll() => await context.Citys.ToListAsync();


        public async Task<City> GetById(int id) => await context.Citys.FindAsync(id);
        

        public async Task<GeneralReponse> Inser(City item)
        {
            if (!await CheckName(item.Name!)) return new GeneralReponse(false, "Deparment already addted");
            context.Citys.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(City item)
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
