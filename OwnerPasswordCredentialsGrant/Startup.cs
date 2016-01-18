using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

[assembly: OwinStartup(typeof(OwnerPasswordCredentialsGrant.Startup))]

namespace OwnerPasswordCredentialsGrant
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888

            //            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            //            {
            //                AuthorizeEndpointPath = new PathString("/authorize"),
            //                TokenEndpointPath = new PathString("/token"),
            //                AuthenticationMode = AuthenticationMode.Passive,
            //                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(1),
            //#if DEBUG
            //                AllowInsecureHttp = true,
            //#endif
            //                ApplicationCanDisplayErrors = true,
            //                Provider = new OAuthAuthorizationServerProvider()
            //                {
            //                    OnValidateClientRedirectUri = context =>
            //                    {
            //                        context.Validated();
            //                        return Task.FromResult(0);
            //                    },
            //                    OnValidateClientAuthentication = context =>
            //                    {
            //                        string clientId;
            //                        string clientSecret;
            //                        if (context.TryGetBasicCredentials(out clientId, out clientSecret) || context.TryGetFormCredentials(out clientId, out clientSecret)) context.Validated();
            //                        return Task.FromResult(0);
            //                    },
            //                },
            //                AuthorizationCodeProvider = new AuthenticationTokenProvider()
            //                {
            //                    OnCreate = context =>
            //                    {
            //                        context.SetToken(DateTime.Now.Ticks.ToString());
            //                        string token = context.Token;
            //                        string ticket = context.SerializeTicket();
            //                        // _authenticationCodes[token] = ticket;
            //                    },
            //                    OnReceive = context =>
            //                    {
            //                        string token = context.Token;
            //                        string ticket;
            //                        //if (_authenticationCodes.TryRemove(token, out ticket))
            //                        //{
            //                        //    context.DeserializeTicket(ticket);
            //                        //}
            //                    },
            //                },
            //                RefreshTokenProvider = new AuthenticationTokenProvider()
            //                {
            //                    OnCreate = context =>
            //                    {
            //                        context.SetToken(context.SerializeTicket());
            //                    },

            //                    OnReceive = context =>
            //                    {
            //                        context.DeserializeTicket(context.Token);
            //                    },
            //                }
            //            });
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new CNBlogsOAuthAuthorizationServerProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            });
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
