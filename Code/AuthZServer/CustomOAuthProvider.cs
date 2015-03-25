using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Security;
using AuthZ.Datalayer;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace AuthZServer
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;

            //first try to get the client details from the Authorization Basic header
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                //no details in the Authorization Header so try to find matching post values
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
            {
                context.SetError("client_not_authorized", "invalid client details");
                return Task.FromResult<object>(null);
            }

            var dataLayer = new RepoManager(new DataLayerDapper()).DataLayer;
            var audienceDto = dataLayer.GetAudience(clientId);

            if (audienceDto == null || !clientSecret.Equals(audienceDto.Secret))
            {
                context.SetError("unauthorized_client", "unauthorized client");
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var identity = new ClaimsIdentity("JWT");                                            
            identity.AddClaim(new Claim("clientID", context.ClientId));
            
            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", context.ClientId 
                    }                    
                });

            var ticket = new AuthenticationTicket(identity, props); 
            context.Validated(ticket);
            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null); 
        }
    }
}