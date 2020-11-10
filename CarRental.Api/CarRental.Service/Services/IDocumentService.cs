using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Service.Services
{
    public interface IDocumentService
    {
        Task SaveFileInFileSystemAsync(IFormFile file, string path);

        Task RemoveDocumentAsync(Guid id);
    }
}
