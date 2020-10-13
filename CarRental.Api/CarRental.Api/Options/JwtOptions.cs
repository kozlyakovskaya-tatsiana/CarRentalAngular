using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.Api.Options
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
