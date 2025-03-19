using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class ProvinceRepository(AplicationContext context) : IGennericRepository<Province>
    {
        public async Task<GeneralReponse> Delete(int id)
        {
            var province = await context.Provinces.FindAsync(id);
            if (province is null) return NotFound();
            context.Provinces.Remove(province);
            await Commit();
            return Sucesss();

        }

        public async Task<List<Province>> GetAll() => await context.Provinces.AsNoTracking().ToListAsync();


        public async Task<Province> GetById(int id) => await context.Provinces.FindAsync(id);


        public async Task<GeneralReponse> Inser(Province item)
        {
            if (!await CheckName(item.NameCity!, item.Id)) return new GeneralReponse(false, "Data not found.");
            context.Provinces.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> Update(Province item)
        {
            var province = await context.Provinces.FindAsync(item.Id);
            if (!await CheckName(item.NameCity!, item.Id)) return new GeneralReponse(false, "Data not found.");
            if (province is null) return NotFound();
            province.NameCity = item.NameCity;
            province.Type = item.Type;
            province.CountryId = item.CountryId;



            await Commit();
            return Sucesss();
        }
        private async Task<bool> CheckName(string name, int id)
        {

            var item = await context.Provinces.FirstOrDefaultAsync(item => item.NameCity!.ToLower().Equals(name.ToLower()) && item.Id != id);
            return item is null;

        }

        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await context.SaveChangesAsync();
    }
}
