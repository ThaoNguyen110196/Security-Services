using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Account;
using Domain.Entities.Entitie.Service;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class CandidateRepository: ICandidateRepository
    {
        private readonly AplicationContext _context;

        public CandidateRepository(AplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Candidate>> GetAllCandidatesAsync()
        {
            return await _context.Candidates.ToListAsync();
        }

        public async Task<Candidate> GetCandidateByIdAsync(int id)
        {
            return await _context.Candidates.FindAsync(id);
        }

        public async Task<GeneralReponse> AddCandidateAsync(Candidate item)
        {
            var checkUser = await FindUserByEmail(item.Email!);
            if (checkUser != null ) return new GeneralReponse(false, "User registered already");
            _context.Candidates.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> UpdateCandidateAsync(Candidate item)
        {
            var obj = await _context.Candidates.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (obj is null) return NotFound();

            if(!await CheckEmail(item.Email!, item.Id)) return Unique();

            obj.Name = item.Name;
            obj.Status = item.Status;
            obj.Phone = item.Phone;

            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> DeleteCandidateAsync(int id)
        {
            var obj = await _context.Candidates.FirstOrDefaultAsync(x => x.Id == id);
            if (obj is null) return NotFound();
            _context.Candidates.Remove(obj);
            await Commit();
            return Sucesss();
        }
        private async Task<bool> CheckEmail(string email, int id)
        {
            var item = await _context.Candidates.FirstOrDefaultAsync(item => item.Email!.ToLower().Equals(email.ToLower()) && item.Id != id);
            return item is null;
        }
        private async Task<Candidate> FindUserByEmail(string email) =>
         await _context.Candidates.FirstOrDefaultAsync(user => user.Email!.ToLower()!.Equals(email!.ToLower()));
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");
        public static GeneralReponse Unique() => new(false, "Data already exists.");

        private async Task Commit() => await _context.SaveChangesAsync();
    }
}
