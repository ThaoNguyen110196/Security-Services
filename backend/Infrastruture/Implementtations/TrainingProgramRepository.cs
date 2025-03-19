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
    public class TrainingProgramRepository: ITrainingProgramRepository
    {
        private readonly AplicationContext _context;

        public TrainingProgramRepository(AplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrainingProgram>> GetAllTrainingProgramsAsync()
        {
            return await _context.TrainingPrograms.ToListAsync();
        }

        public async Task<TrainingProgram> GetTrainingProgramByIdAsync(int id)
        {
            return await _context.TrainingPrograms.FindAsync(id);
        }

        public async Task<GeneralReponse> AddTrainingProgramAsync(TrainingProgram program)
        {
            if (!await CheckName(program.Name!, program.Id)) return Unique();

            _context.TrainingPrograms.Add(program);
            await Commit();
            return Sucesss();
        }
        public async Task<GeneralReponse> UpdateTrainingProgramAsync(TrainingProgram program)
        {
            var item = await _context.TrainingPrograms.FirstOrDefaultAsync(x => x.Id == program.Id);
            if (item is null) return NotFound();
            if (!await CheckName(program.Name!, program.Id)) return Unique();

            item.Description = program.Description;
            item.Name = program.Name;
            item.EndDate = program.EndDate;
            item.StartDate = program.StartDate;
            item.Objectives = program.Objectives;
            item.Instructor = program.Instructor;

            await Commit();
            return Sucesss();
        }


        public async Task<GeneralReponse> DeleteTrainingProgramAsync(int id)
        {
            var item = await _context.TrainingPrograms.FindAsync(id);
            if (item is null) return NotFound();

            _context.TrainingPrograms.Remove(item);
            await Commit();
            return Sucesss();

        }
        private async Task<bool> CheckName(string name, int id )
        {

            var item = await _context.TrainingPrograms.FirstOrDefaultAsync(item => item.Name!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;

        }
        public static GeneralReponse Unique() => new(false, "Data already exists.");
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await _context.SaveChangesAsync();

       

       
    }

    
}

