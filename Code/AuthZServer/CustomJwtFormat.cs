using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.IdentityModel.Tokens;
using AuthZ.Datalayer;
using Thinktecture.IdentityModel.Tokens;

namespace AuthZServer
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string _issuer;

        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            string audienceId = data.Properties.Dictionary.ContainsKey("audience") ? data.Properties.Dictionary["audience"] : null;

            if (string.IsNullOrWhiteSpace(audienceId) || audienceId.Length != 32)
            {
                throw new InvalidOperationException("audience missing from AuthenticationTicket.Properties");
            }

            var dataLayer = new RepoManager(new DataLayerDapper()).DataLayer;

            var audienceDto = dataLayer.GetAudience(audienceId);

            if (audienceDto == null)
            {
                throw new InvalidOperationException("invalid_client");
            }

            var keyByteArray = Convert.FromBase64String(audienceDto.Secret);
            var signingKey = new HmacSigningCredentials(keyByteArray);

            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}