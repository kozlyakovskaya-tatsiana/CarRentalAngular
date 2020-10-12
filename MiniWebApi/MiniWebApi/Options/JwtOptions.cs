using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebApi.JWT
{
    public class JwtOptions
    {
        public const string SectionName = "JwtOptions";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int LifeTime { get; set; }

        public int RefreshTokenLifeTime { get; set; }

        public string Key { get; set; }

        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));

    }
}
