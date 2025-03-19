
using Amazon.S3;
using Amazon.S3.Model;
using Aplication.Contracts;
using Microsoft.AspNetCore.Http;

namespace Infrastruture.Services
{
    public  class S3Service : IS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3Service(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
            _bucketName = "your-s3-bucket-name"; // Thay thế bằng tên bucket của bạn
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file.Length > 0)
            {
                var fileKey = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                using (var stream = file.OpenReadStream())
                {
                    var putRequest = new PutObjectRequest
                    {
                        BucketName = _bucketName,
                        Key = fileKey,
                        InputStream = stream,
                        ContentType = file.ContentType
                    };
                    await _s3Client.PutObjectAsync(putRequest);
                }

                return $"https://{_bucketName}.s3.amazonaws.com/{fileKey}";
            }

            throw new InvalidOperationException("No file to upload");
        }

        public Task<string> GetImageUrlAsync(string fileKey)
        {
            var url = $"https://{_bucketName}.s3.amazonaws.com/{fileKey}";
            return Task.FromResult(url);
        }
    }
}
