using System;
using System.Collections.Generic;
using System.Linq;
using Aplication.Contracts;
using Domain.Entities.Entitie.Service;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastruture.Services
{
    public class LogService:ILogServiceEntity
    {
        private readonly AplicationContext _context;

        public LogService(AplicationContext context)
        {
            _context = context;
        }
        public async Task LogDeletionAsync<T>(T entity, string details, DateTime deletionDate)
        {
            var logEntry = new DeletionLog { 
                EntityType = typeof(T).Name,
             
                Details = details,
                DeletionDate = deletionDate
            };
            _context.DeletionLog.Add(logEntry);
            await _context.SaveChangesAsync();
        }

        // Xóa log theo thời gian chỉ định
        public async Task DeleteOldLogsAsync(TimeSpan logRetentionPeriod)
        {
            var cutoffDate = DateTime.UtcNow - logRetentionPeriod;
            var oldLogs = _context.DeletionLog.Where(log => log.DeletionDate < cutoffDate);
            _context.DeletionLog.RemoveRange(oldLogs);
            await _context.SaveChangesAsync();
        }

        // Truy xuất log
        public async Task<List<DeletionLog>> GetLogsAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.DeletionLog.AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(log => log.DeletionDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(log => log.DeletionDate <= endDate.Value);
            }

            return await query.ToListAsync();
        }

        // Helper method to extract entity ID
        private int GetEntityId<T>(T entity)
        {
            var propertyInfo = typeof(T).GetProperty("Id");
            if (propertyInfo != null)
            {
                return (int)propertyInfo.GetValue(entity);
            }
            throw new InvalidOperationException("Entity does not have an Id property");
        }


    }
}
