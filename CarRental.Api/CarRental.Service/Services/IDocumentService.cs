using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using CarRental.DAL.Entities;

namespace CarRental.Service.Services
{
    public interface IDocumentService
    {
        Task SaveFileInFileSystemAsync(IFormFile file, string path);

        Task RemoveAsync(Guid id);

        void SetUniqueNameAndPath(Document document, string fileName, string filePath);
    }
}
