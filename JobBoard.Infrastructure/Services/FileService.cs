using JobBoard.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace JobBoard.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task<string> SaveFileAsync(byte[] fileData, string fileName, string folderName)
        {
             // E.g., wwwroot/uploads/cvs
            var uploadsFolder = Path.Combine(_environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "uploads", folderName);
            
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            // Create a unique file name to prevent overwriting: GUID_filename.pdf
            var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            await File.WriteAllBytesAsync(filePath, fileData);
            // Return relative path to save in DB (e.g., "uploads/cvs/uuid_file.pdf")
            return Path.Combine("uploads", folderName, uniqueFileName).Replace("\\", "/");
        }
        public void DeleteFile(string filePath)
        {
            var fullPath = Path.Combine(_environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), filePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}