using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniWebApi.Options
{
    public class OpenApiSecurityScehemeOptions
    {
        public const string SectionName = "OpenApiSecurityScehemeOptions";

        public string Title { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }
    }
}
