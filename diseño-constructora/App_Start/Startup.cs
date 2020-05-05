using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Builder;

[assembly: OwinStartup(typeof(diseño_constructora.App_Start.Startup))]

namespace diseño_constructora.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            _ = app.UseCookieAuthentication(
                new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions()
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    LoginPath = new PathString("/Login/Entrar")
                });

            app.UseExternalSignInCookie(
                DefaultAuthenticationTypes.ExternalCookie);

            //app.UseFacebookAuthentication(
            //    appId: "954619978030994", appSecret: "534a771e21b47a324e1198ac4df0ab19");

            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "123", clientSecret: "12345");

            //app.UseGoogleAuthentication(
            //    clientId: "123", clientSecret: "1235");

            //app.UseTwitterAuthentication(
            //    consumerKey: "123", consumerSecret: "15");

        }
    }
}