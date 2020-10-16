using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.Service.Identity.Options
{
    public class JwtOptions
    {
        public const string SectionName = "JwtOptions";

        public string Issuer { get; set; }

        public int LifeTime { get; set; }

        public int RefreshTokenLifeTime { get; set; }

        public string Key { get; set; }

        public string RefreshIssuer { get; set; }

        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));

    }
}
