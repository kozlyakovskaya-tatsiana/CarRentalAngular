using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniWebApi.Options
{
    public class SwaggerDocumentOptions
    {
        public const string SectionName = "DocumentOptions";

        public string DocumentName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }
    }
}
