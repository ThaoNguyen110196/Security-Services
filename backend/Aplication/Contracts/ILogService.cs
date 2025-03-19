using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entitie.Service;

namespace Aplication.Contracts
{
    public interface ILogServiceEntity
    {
        // Ghi log xóa đối tượng
        Task LogDeletionAsync<T>(T entity, string details, DateTime deletionDate);

        // Xóa log cũ theo khoảng thời gian giữ log
        Task DeleteOldLogsAsync(TimeSpan logRetentionPeriod);

        // Truy xuất log theo khoảng thời gian
        Task<List<DeletionLog>> GetLogsAsync(DateTime? startDate = null, DateTime? endDate = null);
    }
}
