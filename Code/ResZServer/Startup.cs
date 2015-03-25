using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ApiSupport;
using AuthZ.Datalayer;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Owin;
using Microsoft.Owin.Security;

namespace ResZServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();            
            this.ConfigureOAuth(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);            
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var audiences = new RepoManager(new DataLayerDapper()).DataLayer.GetAll();

            var jwtAudiences = new List<string>();
            var tokenProviders = new List<IIssuerSecurityTokenProvider>();

            var issuer = ConfigurationHelper.GetAppSetting("TokenIssuer");

            foreach (var audienceTemp in audiences)
            {
                jwtAudiences.Add(audienceTemp.ClientId);
                tokenProviders.Add(new SymmetricKeyIssuerSecurityTokenProvider(issuer, TextEncodings.Base64Url.Decode(audienceTemp.Secret)));
            }

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = jwtAudiences,
                    IssuerSecurityTokenProviders = tokenProviders
                });
        }
    }
}
