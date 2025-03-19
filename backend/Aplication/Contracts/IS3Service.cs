using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Aplication.Contracts
{
    public interface IS3Service
    {
        Task<string> UploadImageAsync(IFormFile file);
        Task<string> GetImageUrlAsync(string fileKey);
    }
}
