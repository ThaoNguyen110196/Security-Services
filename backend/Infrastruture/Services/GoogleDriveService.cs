using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Contracts;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Infrastruture.Helper;

namespace Infrastruture.Services
{
    public class GoogleDriveService:IGoogleDriveService
    {
        private readonly DriveService _driveService;

        public GoogleDriveService(IConfiguration configuration)
        {
            // Đọc cấu hình từ appsettings.json hoặc từ nơi khác
            var serviceAccountKeyFilePath = configuration["GoogleDriveSettings:ServiceAccountKeyPath"];
            var applicationName = configuration["GoogleDriveSettings:ApplicationName"];

            // Tạo credential từ tệp JSON
            var clientSecrets = GoogleClientSecrets.FromFile(serviceAccountKeyFilePath).Secrets;
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
             clientSecrets,
             new[] { DriveService.ScopeConstants.DriveFile },
             "user",
             CancellationToken.None).Result;

            // Khởi tạo DriveService
            _driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName,
            });
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderId = null)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is required");
            }

            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = file.FileName,
                MimeType = file.ContentType
            };

            if (!string.IsNullOrEmpty(folderId))
            {
                fileMetadata.Parents = new List<string> { folderId };
            }

            try
            {
                FilesResource.CreateMediaUpload request;
                using (var stream = file.OpenReadStream())
                {
                    request = _driveService.Files.Create(fileMetadata, stream, file.ContentType);
                    request.Fields = "id";
                    var uploadResult = await request.UploadAsync();

                    if (uploadResult.Status != Google.Apis.Upload.UploadStatus.Completed)
                    {
                        throw new Exception($"File upload failed: {uploadResult.Exception?.Message ?? "Unknown error"}");
                    }
                }

                var fileId = request.ResponseBody?.Id;
                if (fileId == null)
                {
                    throw new Exception("File ID is null. Upload might have failed.");
                }

                return fileId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading file: {ex.Message}", ex);
            }

           
        }

        public async Task<string> GetFileUrlAsync(string fileId)
        {
            var request = _driveService.Files.Get(fileId);
            var file = await request.ExecuteAsync();
            return file.WebViewLink;
        }

        public async Task<bool> DeleteFileAsync(string fileId)
        {
            try
            {
                var request = _driveService.Files.Delete(fileId);
                await request.ExecuteAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete file: {ex.Message}");
            }
        }
    }
}

