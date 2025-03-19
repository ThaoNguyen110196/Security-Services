
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Service;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class InterviewRepository: IInterviewRepository
    {
        private readonly AplicationContext _context;

        public InterviewRepository(AplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Interview>> GetAllInterviewsAsync()
        {
            return await _context.Interviews.Include(i => i.Candidate).ToListAsync();
        }

        public async Task<Interview> GetInterviewByIdAsync(int id)
        {
            return await _context.Interviews.Include(i => i.Candidate).FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Interview> GetInterviewByCandidateIdAsync(int candidateId)
        {
            return await _context.Interviews.FirstOrDefaultAsync(x => x.CandidateID == candidateId);
        }
        public async Task<GeneralReponse> AddInterviewAsync(Interview item)
        {

            if (!await CheckName(item.Id!)) return NotFound();
            _context.Interviews.Add(item);
            await Commit();
            return Sucesss();

        }

        public async Task<GeneralReponse> UpdateInterviewAsync(Interview item)
        {
            if (!await CheckName(item.Id!)) return NotFound();
            _context.Interviews.Update(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> DeleteInterviewAsync(int id)
        {
            var item = await _context.Interviews.FindAsync(id);
            if (item is null) return NotFound();
            _context.Interviews.Remove(item);
            await Commit();
            return Sucesss();
        }
        private async Task<bool> CheckName(int id)
        {

            var item = await _context.Interviews.FirstOrDefaultAsync(item => item.Id == id);
            return item is null;

        }
        public static GeneralReponse NotFound() => new(false, "Sorry deparment not found");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await _context.SaveChangesAsync();
    }
}
