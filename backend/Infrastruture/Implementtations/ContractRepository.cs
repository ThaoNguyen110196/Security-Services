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
    public class ContractRepository(AplicationContext context) : IGennericRepository<Contract>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var item = await context.Contracts.FindAsync(id);
            if (item is null) return NotFound();
            context.Contracts.Remove(item);
            await Commit();
            return Sucesss();
        }

        public async Task<List<Contract>> GetAll()=> await context.Contracts.AsNoTracking().ToListAsync();
        

        public async Task<Contract> GetById(int id) => await context.Contracts.FindAsync(id);
       

        public async Task<GeneralReponse> Inser(Contract item)
        {
            context.Contracts.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(Contract item)
        {
            var contract = await context.Contracts.FindAsync(item.Id);
            if (contract is null) return NotFound();
            contract.EndDate = item.EndDate;
            contract.StartDate = item.StartDate;
            contract.Content = item.Content;
            contract.ContractNumber = item.ContractNumber;

           await Commit();
            return Sucesss();
        }
        
        public static GeneralReponse NotFound() => new(false, "Sorry deparment not found");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
