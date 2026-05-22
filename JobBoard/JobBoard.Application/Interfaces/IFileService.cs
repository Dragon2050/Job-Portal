using JobBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Application.Interfaces
{
    public interface IFileService
    {
        //returns the saved file path
        Task<string> SaveFileAsync(byte[] fileData, string fileName, string folderName);
        void DeleteFile(string filePath);
    }
}