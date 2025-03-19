
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastruture.Implementtations
{
    public class DistrictRepository(AplicationContext context) : IGennericRepository<District>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var district = await context.Districts.FindAsync(id);
            if(district is null) return NotFound();
            context.Districts.Remove(district);
            await Commit();
            return Sucesss();
        }

        public async Task<List<District>> GetAll() => await context.Districts.ToListAsync(); 
       

        public async Task<District> GetById(int id) => await context.Districts.FindAsync(id);


        public async Task<GeneralReponse> Inser(District item)
        {
            if (!await CheckName(item.NameDistrict!,item.Id)) return new GeneralReponse(false, "Data already exists.");
            context.Districts.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(District item)
        {
            var district = await context.Districts.FindAsync(item.Id);
            if (!await CheckName(item.NameDistrict!,item.Id)) return new GeneralReponse(false, "Data already exists.");
            

            if (district is null) return NotFound();
            district.NameDistrict = item.NameDistrict;
            district.Type = item.Type;
            district.ProvinceId = item.ProvinceId;

            await Commit();
            return Sucesss();
        }
        private async Task<bool> CheckName(string name, int id)
        {

            var item = await context
                 .Districts
                 .FirstOrDefaultAsync(item => item.NameDistrict!.ToLower().Equals(name.ToLower())&& item.Id!= id);
       
            return item is null;

        }
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
