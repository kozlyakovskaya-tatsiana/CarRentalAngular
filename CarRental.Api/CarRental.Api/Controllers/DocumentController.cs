using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRental.Api.Controllers
{
    [Authorize(Policy = "ForManagersAdmins")]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly ILogger<DocumentController> _logger;

        private readonly IMapper _mapper;

        private readonly IDocumentService _documentService;

        public DocumentController(ILogger<DocumentController> logger, IMapper mapper, IDocumentService documentService)
        {
            _logger = logger;

            _mapper = mapper;

            _documentService = documentService;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveDocument(Guid id)
        {
            await _documentService.RemoveDocumentAsync(id);

            return Ok();
        }
    }
}
