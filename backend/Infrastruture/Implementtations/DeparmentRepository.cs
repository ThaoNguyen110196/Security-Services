using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class DeparmentRepository(AplicationContext context) : IGennericRepository<Departnent>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var dep = await context.Departners.FindAsync(id);
            if (dep is null) return NotFound();
            context.Departners.Remove(dep);
            await Commit();
            return Sucesss();
        }

        public async Task<List<Departnent>> GetAll()=> await context.Departners.ToListAsync();

        public async Task<Departnent> GetById(int id) => await context.Departners.FindAsync(id);
        
        public async Task<GeneralReponse> Inser(Departnent item)
        {
            if (!await CheckName(item.Name!, item.Id)) return Unique();
            context.Departners.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(Departnent item)
        {
            var dep = await context.Departners.FindAsync(item.Id);
            if (dep is null) return NotFound();

            if (!await CheckName(item.Name!, item.Id)) return Unique();

            dep.Name  = item.Name;
            dep.GeneralDepartmentId = item.GeneralDepartmentId;

            context.Departners.Update(dep);
            await Commit();
            return Sucesss();
        }
        private async Task<bool> CheckName(string name, int id)
        {
            var item = await context.Departners.FirstOrDefaultAsync(item => item.Name!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;
        }

        public static GeneralReponse Unique() => new(false, "Data already exists.");
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();

    }
}
