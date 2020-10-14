using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using SIFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace SIFCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddMvc();

             services.AddDbContext<SIFContext>();
            
            services.AddAuthentication( "Cookies") // Sets the default scheme to cookies
                .AddCookie( "Cookies", options =>
                {
                    options.AccessDeniedPath = "/account/denied";
                    options.LoginPath = "/account/login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.SlidingExpiration = true;                    
                });                
                // .AddCAS(o =>
                // {
                //     o.CasServerUrlBase = Configuration["CasBaseUrl"];   // Set in `appsettings.json` file.
                //     o.SignInScheme = "Cookies";
                //     o.Events.OnTicketReceived = async context => {
                //         var identity = (ClaimsIdentity) context.Principal.Identity;
                //         if (identity == null)
                //         {
                //             return;
                //         }

                //         // kerb comes across in name & name identifier
                //         var kerb = identity?.FindFirst(ClaimTypes.NameIdentifier).Value;

                //         if (string.IsNullOrWhiteSpace(kerb)) return;

                //         var identityService = services.BuildServiceProvider().GetService<IIdentityService>();

                //         var user = await identityService.GetByKerberos(kerb);

                //         if (user == null)
                //         {
                //             return;
                //         }                        

                //         identity.RemoveClaim(identity.FindFirst(ClaimTypes.Name));
                //         identity.AddClaim(new Claim(ClaimTypes.Name, user.Id));

                //         identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
                //         identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
                //         identity.AddClaim(new Claim("name", user.Name));
                //         identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                //         identity.RemoveClaim(identity.FindFirst(ClaimTypes.NameIdentifier));
                //         identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, kerb));
                //         identity.AddClaim(new Claim(ClaimTypes.Role, "Employee"));
                //         if(!user.SeasonalEmployee)
                //         {
                //             identity.AddClaim(new Claim(ClaimTypes.Role, "AllowEmulate"));
                //         }

                //         context.Principal.AddIdentity(identity);

                //         await Task.FromResult(0); 
                //     };
                // });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }        

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication(); 

            app.UseMvc(routes =>
            {
                routes.MapAreaRoute(
                   name: "Client_route",
                   areaName: "Client",
                   template:  "client/{controller}/{action=Index}/{id?}"
               );

               routes.MapAreaRoute(
                   name: "Admin_route",
                   areaName: "Admin",
                   template:  "admin/{controller}/{action=Index}/{id?}"
               );
               
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
