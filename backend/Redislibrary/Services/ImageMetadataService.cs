using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redislibrary.Services
{
    public interface ImageMetadataService
    {
        Task InsertImageMetadataAsync(ImageMetadata metadata);
        Task<ImageMetadata> GetImageMetadataAsync(string id);
    }
}
