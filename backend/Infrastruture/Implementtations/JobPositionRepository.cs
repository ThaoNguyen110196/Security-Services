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
    public class JobPositionRepository: IJobPositionRepository
    {
        private readonly AplicationContext _context;

        public JobPositionRepository(AplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobPosition>> GetAllJobPositionsAsync()
        {
            return await _context.JobPositions.ToListAsync();
        }

        public async Task<JobPosition> GetJobPositionByIdAsync(int id)
        {
            return await _context.JobPositions.FindAsync(id);
        }

        public async Task<GeneralReponse> AddJobPositionAsync(JobPosition item)
        {
            if (!await CheckName(item.PositionName!)) return NotFound();
            _context.JobPositions.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> UpdateJobPositionAsync(JobPosition item)
        {
            var obj = await _context.JobPositions.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (obj is null) return NotFound();
            obj.ApplicationDeadline = item.ApplicationDeadline;
            obj.JobDescription = item.JobDescription;
            obj.PositionName = item.PositionName;
            obj.Status = item.Status;
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> DeleteJobPositionAsync(int id)
        {
            var item = await _context.JobPositions.FindAsync(id);

            if (item is null) return NotFound();
            _context.JobPositions.Remove(item);
            await Commit();
            return Sucesss();
        }
        private async Task<bool> CheckName(string name)
        {

            var item = await _context.JobPositions.FirstOrDefaultAsync(item => item.PositionName!.ToLower().Equals(name.ToLower()));
            return item is null;

        }
        public static GeneralReponse NotFound() => new(false, "Sorry JobPositions not found");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await _context.SaveChangesAsync();
    }
}
