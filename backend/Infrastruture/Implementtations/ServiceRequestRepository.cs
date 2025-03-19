using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Domain.Entities.Entitie.Service;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Implementtations
{
    public class ServiceRequestRepository: IServiceRequestRepository
    {
        private readonly AplicationContext _context;

        public ServiceRequestRepository(AplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceRequest>> GetAllServiceRequestsAsync()
        {
            return await _context.ServiceRequests.Include(sr => sr.Customer).ToListAsync();
        }

        public async Task<ServiceRequest> GetServiceRequestByIdAsync(int id)
        {
            return await _context.ServiceRequests.Include(sr => sr.Customer).FirstOrDefaultAsync(sr => sr.Id == id);
        }

        public async Task<GeneralReponse> AddServiceRequestAsync(ServiceRequest item)
        {
            if (item is null) return NotFound(); 
            _context.ServiceRequests.Add(item);
            await Commit();
            return Sucesss();
        }

          
        public async Task<GeneralReponse> UpdateServiceRequestAsync(ServiceRequest item)
        {
            var obj = await _context.ServiceRequests.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (obj is null) return NotFound();

            obj.Status = item.Status;
            obj.Name = item.Name;
            obj.CustomerID = item.CustomerID;
            obj.ServiceType = item.ServiceType;
            obj.RequestDetails = item.RequestDetails;
           
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> DeleteServiceRequestAsync(int id)
        {
            var obj = await _context.ServiceRequests.FirstOrDefaultAsync(x => x.Id == id);
            if (obj is null) return NotFound();
            _context.ServiceRequests.Remove(obj);
            await Commit();
            return Sucesss();
        }
        public static GeneralReponse NotFound() => new(false, "Sorry Servicereqest not found");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await _context.SaveChangesAsync();

        public async Task<IEnumerable<ServiceRequest>> GetServiceRequestsByCustomerIdAsync(int id)
        {
            return await _context.ServiceRequests
            .Where(sr => sr.CustomerID == id)
            .ToListAsync();
        }
    }
}
