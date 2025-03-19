using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Service;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class CashTransactionRepository : ICashTransactionRepository
    {
        private readonly AplicationContext _context;

        public CashTransactionRepository(AplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CashTransaction>> GetAllCashTransactionsAsync()
        {
            return await _context.CashTransactions.ToListAsync();
        }

        public async Task<CashTransaction> GetCashTransactionByIdAsync(int id)
        {
            return await _context.CashTransactions.FirstOrDefaultAsync(ct => ct.Id == id);
        }

        public async Task<GeneralReponse> AddCashTransactionAsync(CashTransaction item)
        {
            _context.CashTransactions.Add(item);
            await Commit();
            return Sucesss();
        }


        public async Task<GeneralReponse> UpdateCashTransactionAsync(CashTransaction item)
        {
            var obj = await _context.CashTransactions.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (obj is null) return NotFound();

            obj.Amount = item.Amount;
            obj.CashServiceID = item.CashServiceID;
            obj.EmployeeID = item.EmployeeID;
            obj.Status = item.Status;
            obj.TransactionDate = item.TransactionDate;

            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> DeleteCashTransactionAsync(int id)
        {
            var obj = await _context.CashTransactions.FirstOrDefaultAsync(x => x.Id == id);
            if (obj is null) return NotFound();
            _context.CashTransactions.Remove(obj);
            await Commit();
            return Sucesss();
        }
        public static GeneralReponse NotFound() => new(false, "Sorry CashTransactions not found");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await _context.SaveChangesAsync();

        public async Task<IEnumerable<CashTransaction>> GetByCashServiceIdAsync(int cashServiceId)
        {
            if (cashServiceId <= 0)
            {
                throw new ArgumentException("Invalid CashServiceId", nameof(cashServiceId));
            }

            return await _context.CashTransactions
                          .Include(ct => ct.CashService) // Nạp CashService
                          .Where(ct => ct.CashServiceID == cashServiceId)
                          .ToListAsync();
        }
    }
}
