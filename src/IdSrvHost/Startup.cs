using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(IdSrvHost.Startup))]
namespace IdSrvHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            app.UseIdentityServer(new IdentityServerOptions
            {
                SiteName = "NetPonto Demo IdentityServer3",

                SigningCertificate = LoadCertificate(),                

                Factory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(InMemoryConfig.Clients)
                    .UseInMemoryUsers(InMemoryConfig.Users)
                    .UseInMemoryScopes(InMemoryConfig.Scopes),

                AuthenticationOptions = new AuthenticationOptions
                {
                    EnableLocalLogin = true,

                    IdentityProviders = (authApp, type) => 
                    {
                        authApp.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
                        {
                            AuthenticationType = "Google",
                            Caption = "Google",                            
                            ClientId = insert_google_client_id_here,
                            ClientSecret = insert_google_client_secret_here,

                            SignInAsAuthenticationType = type,
                        });


                        var facebookOptions = new FacebookAuthenticationOptions
                        {
                            AuthenticationType = "Facebook",
                            Caption = "Facebook",
                            AppId = insert_facebook_client_id_here,
                            AppSecret = insert_facebook_client_secret_here,              

                            SignInAsAuthenticationType = type,                            
                        };
                        facebookOptions.Scope.Add("email");
                        facebookOptions.Scope.Add("public_profile");
                        authApp.UseFacebookAuthentication(facebookOptions);                        
                    }
                },
            });            
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\bin\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }

    }
   
}