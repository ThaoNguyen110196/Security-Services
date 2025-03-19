using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Domain.Entities.Entitie.Service;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class TestimonialRepository(AplicationContext context) : IGennericRepository<Testimonial>
    {

        private readonly List<string> _invalidWords = new List<string> { "fuck", "shit", "damn" };

        private bool ContainsInvalidWords(string input)
        {
            return _invalidWords.Any(word => input.Contains(word, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<GeneralReponse> Delete(int id)
        {
            var obj = await context.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
            if (obj is null) return NotFound();
            context.Testimonials.Remove(obj);
            await Commit();
            return Sucesss();
        }

        public async Task<List<Testimonial>> GetAll() => await context.Testimonials.ToListAsync();

        public async Task<Testimonial> GetById(int id) => await context.Testimonials
            .FirstOrDefaultAsync(x => x.Id == id);


        public async Task<GeneralReponse> Inser(Testimonial item)
        {
            if (ContainsInvalidWords(item.Desc)) return BadRequest();

            if (!await CheckTestimonial(item.CustomerId)) return Unique();

            context.Testimonials.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(Testimonial item)
        {
            if (ContainsInvalidWords(item.Desc)) return BadRequest();

            var obj = await context.Testimonials.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (obj is null) return NotFound();

            obj.Star = item.Star;
            obj.Desc = item.Desc;

            context.Testimonials.Update(obj);
            await Commit();
            return Sucesss();
        }

        private async Task<bool> CheckTestimonial(int customerId)
        {
            var existingTestimonial = await context.Testimonials
                .FirstOrDefaultAsync(t => t.CustomerId == customerId);
            return existingTestimonial is null;
        }

        public static GeneralReponse Unique() => new (false, "Customer already has a testimonial.");

        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse BadRequest() => new(false, "The testimonial contains inappropriate language.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
