using System;
using System.IO;
using System.Threading.Tasks;
using CarRental.DAL;
using CarRental.DAL.Entities;
using CarRental.DAL.Exceptions;
using Microsoft.AspNetCore.Http;

namespace CarRental.Service.Services.Models
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<Document> _documentRepository;

        public DocumentService(IRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task SaveFileInFileSystemAsync(IFormFile file, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public async Task RemoveDocumentAsync(Guid id)
        {
            var doc = await _documentRepository.FindByIdAsync(id);

            if (doc == null)
                throw new NotFoundException("There is no document with such Id");

            var docPath = doc.Path;

            File.Delete(docPath);

            await _documentRepository.RemoveAsync(id);
        }
    }
}
