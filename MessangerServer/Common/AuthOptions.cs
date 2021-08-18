using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MessangerServer.Common
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int TokenLifetime { get; set; }

        public SymmetricSecurityKey GetSemetricKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
