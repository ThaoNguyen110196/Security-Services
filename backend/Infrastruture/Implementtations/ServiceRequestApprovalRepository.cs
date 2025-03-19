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
    public class ServiceRequestApprovalRepository: IServiceRequestApprovalRepository
    {
        private readonly AplicationContext _context;

        public ServiceRequestApprovalRepository(AplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceRequestApproval>> GetAllServiceRequestApprovalsAsync()
        {
            return await _context.ServiceRequestApprovals.Include(sra => sra.ServiceRequest).ToListAsync();
        }

        public async Task<ServiceRequestApproval> GetServiceRequestApprovalByIdAsync(int id)
        {
            return await _context.ServiceRequestApprovals.Include(sra => sra.ServiceRequest).FirstOrDefaultAsync(sra => sra.Id == id);
        }

        public async Task AddServiceRequestApprovalAsync(ServiceRequestApproval approval)
        {
            await _context.ServiceRequestApprovals.AddAsync(approval);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateServiceRequestApprovalAsync(ServiceRequestApproval approval)
        {
            _context.ServiceRequestApprovals.Update(approval);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteServiceRequestApprovalAsync(int id)
        {
            var approval = await _context.ServiceRequestApprovals.FindAsync(id);
            if (approval != null)
            {
                _context.ServiceRequestApprovals.Remove(approval);
                await _context.SaveChangesAsync();
            }
        }
    }
    public class ServiceScheduleRepository : IServiceScheduleRepository
    {
        private readonly AplicationContext _context;

        public ServiceScheduleRepository(AplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServiceSchedule>> GetAllServiceSchedulesAsync()
        {
            return await _context.ServiceSchedules.Include(ss => ss.ServiceRequest).ToListAsync();
        }

        public async Task<ServiceSchedule> GetServiceScheduleByIdAsync(int id)
        {
            return await _context.ServiceSchedules.Include(ss => ss.ServiceRequest).FirstOrDefaultAsync(ss => ss.Id == id);
        }

        public async Task<GeneralReponse> AddServiceScheduleAsync(ServiceSchedule item)
        {

            if(item is null)return NotFound();
            _context.ServiceSchedules.Add(item);
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> UpdateServiceScheduleAsync(ServiceSchedule item)
        {
            var obj = await  _context.ServiceSchedules.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (obj is null) return NotFound();
            obj.ScheduledDate = item.ScheduledDate;
            obj.EmployeeID = item.EmployeeID;
            obj.Location = item.Location;
            obj.Name = item.Name;
            obj.ServiceRequestID = item.ServiceRequestID;
           
            await Commit();
            return Sucesss();
        }

        public async Task<GeneralReponse> DeleteServiceScheduleAsync(int id)
        {
            var obj = await _context.ServiceSchedules.FirstOrDefaultAsync(x => x.Id == id);
            if (obj is null) return NotFound();
            _context.ServiceSchedules.Remove(obj);
            await Commit();
            return Sucesss();
        }
        public static GeneralReponse Unique() => new(false, "Data already exists.");
        public static GeneralReponse NotFound() => new(false, "Data not found.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await _context.SaveChangesAsync();
    }

}
