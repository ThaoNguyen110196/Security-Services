using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Contracts;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Infrastruture.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastruture.Services
{
    public class CloundinaryService : ICloudinaryInterface
    {
        private readonly Cloudinary _cloudinary;

        public CloundinaryService(IConfiguration configuration)
        {
            var cloudinarySettings = configuration.GetSection("CloudinarySettings");
            Account account = new Account(
                cloudinarySettings["CloudName"],
                cloudinarySettings["ApiKey"],
                cloudinarySettings["ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folderName = "default")
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = folderName,
                Overwrite = true
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new Exception($"Failed to upload image: {uploadResult.Error.Message}");
            }

            return uploadResult.SecureUrl.ToString();
        }
        public async Task<string> UploadDocxAsync(IFormFile file, string folderName = "default")
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is required");
            }

            // Kiểm tra định dạng tệp
            var allowedExtensions = new[] { ".docx" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("Only DOCX files are allowed");
            }

            try
            {
                // Tạo các tham số tải lên cho Cloudinary
                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    Folder = folderName,
                    PublicId = Guid.NewGuid().ToString(),  // Tạo ID công khai duy nhất
                    RawConvert = "aspose" // Chuyển đổi DOCX sang PDF (hoặc định dạng khác nếu cần)
                };

                // Tải tệp lên Cloudinary
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    throw new Exception($"Failed to upload and convert file: {uploadResult.Error.Message}");
                }

                // Trả về URL an toàn của tệp đã tải lên
                return uploadResult.SecureUrl.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading or converting file: {ex.Message}");
            }
        }
        public async Task<string> UploadPdfAsync(IFormFile file, string folderName = "pdf-files")
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is required");
            }

            // Kiểm tra định dạng tệp
            var allowedExtensions = new[] { ".pdf" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("Only PDF files are allowed");
            }

            try
            {
                // Tạo các tham số tải lên cho Cloudinary
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    Folder = folderName,
                    PublicId = Guid.NewGuid().ToString(),  // Tạo ID công khai duy nhất
                    Ocr = "adv_ocr" // Yêu cầu OCR nâng cao
                };

                // Tải tệp lên Cloudinary
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    throw new Exception($"Failed to upload and OCR file: {uploadResult.Error.Message}");
                }

                // Trả về URL an toàn của tệp đã tải lên hoặc các thông tin OCR
                return uploadResult.JsonObj["url"].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading or OCR file: {ex.Message}");
            }
        }
        public async Task<bool> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var deleteResult = await _cloudinary.DestroyAsync(deleteParams);

            if (deleteResult.Error != null)
            {
                throw new Exception($"Failed to delete image: {deleteResult.Error.Message}");
            }

            return deleteResult.Result == "ok";
        }
    }
}
