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
    public class TrainingHistoryRepository : ITrainingHistoryRepository
    {
        private readonly AplicationContext _context;

        public TrainingHistoryRepository(AplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrainingHistory>> GetAllTrainingHistoriesAsync()
        {
            return await _context.TrainingHistories.Include(th => th.TrainingProgram).ToListAsync();
        }

        public async Task<TrainingHistory> GetTrainingHistoryByIdAsync(int id)
        {
            return await _context.TrainingHistories.Include(th => th.TrainingProgram).FirstOrDefaultAsync(th => th.Id == id);
        }

        public async Task<GeneralReponse> AddTrainingHistoryAsync(TrainingHistory item)
        {
            if (!await CheckName(item.Name!, item.Id)) return Unique();

            _context.TrainingHistories.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> UpdateTrainingHistoryAsync(TrainingHistory item)
        {
            var obj = await _context.TrainingHistories.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (obj is null) return NotFound();

            if (!await CheckName(item.Name!, item.Id)) return Unique();

            obj.CompletionStatus = item.CompletionStatus;
            obj.EmployeeID = item.EmployeeID;
            obj.ProgramID = item.ProgramID;
            obj.Name = item.Name;
            obj.CompletionDate = item.CompletionDate;

            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> DeleteTrainingHistoryAsync(int id)
        {
            var obj = await _context.TrainingHistories.FirstOrDefaultAsync(x => x.Id == id);
            if (obj is null) return NotFound();
            _context.TrainingHistories.Remove(obj);
            await Commit();
            return Sucesss();
        }

        private async Task<bool> CheckName(string name, int id)
        {
            var item = await _context.TrainingHistories.FirstOrDefaultAsync(item => item.Name!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;
        }

        public static GeneralReponse Unique() => new(false, "Data already exists.");
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await _context.SaveChangesAsync();
    }
}

