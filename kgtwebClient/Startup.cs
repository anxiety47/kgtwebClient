using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

[assembly: OwinStartup(typeof(kgtwebClient.Startup))]

namespace kgtwebClient
{
    public class Startup
    {

        // ------------

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(o =>
            {
                o.ClientId = "764570344921-1g1rfqr8dho33o7m7srb9lof22dguq6t.apps.googleusercontent.com";
                o.ClientSecret = "W1zAXLxlW8qVPHwXCLAwFJGy";
                o.Authority = "https://accounts.google.com";
                o.ResponseType = OpenIdConnectResponseType.Code;
                o.GetClaimsFromUserInfoEndpoint = true;
                o.SaveTokens = true;
                o.Events = new OpenIdConnectEvents()
                {
                    OnRedirectToIdentityProvider = (context) =>
                    {
                        if (context.Request.Path != "/account/external")
                        {
                            context.Response.Redirect("/account/login");
                            context.HandleResponse();
                        }

                        return Task.FromResult(0);
                    }
                };
            }
            );

            services.AddMvc();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                                      ILoggerFactory loggerFactory)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //}

            //app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
            
        }
    }
}