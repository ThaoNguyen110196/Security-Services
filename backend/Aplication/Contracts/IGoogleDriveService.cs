using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Aplication.Contracts
{
    public interface IGoogleDriveService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderId = null);
        Task<string> GetFileUrlAsync(string fileId);
        Task<bool> DeleteFileAsync(string fileId);  // Thêm phương thức xóa tệp
    }
}
