using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniWebApi.Options
{
    public class SecurityDefinitionOptions
    {
        public const string SectionName = "SecurityDefinitionOptions";

        public string SecuriyDefinitionName { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string Scheme { get; set; }
    }
}
