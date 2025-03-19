using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Aplication.Contracts
{
    public interface ICloudinaryInterface
    {
        Task<string> UploadImageAsync(IFormFile file, string folderName);
        Task<string> UploadPdfAsync(IFormFile file, string folderName);
        Task<bool> DeleteImageAsync(string fileKey);
    }
}
