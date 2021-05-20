using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SIFCore.Models;
using System.Security.Claims;
using SIFCore.Services;

namespace SIFCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            
            services.AddControllersWithViews();  

            IMvcBuilder builder = services.AddRazorPages(); 

            #if DEBUG
                if (Env.IsDevelopment())
                {
                    builder.AddRazorRuntimeCompilation();
                }
            #endif        

            services.AddDbContext<SIFContext>();
      
            services.AddAuthentication("Cookies") // Sets the default scheme to cookies
                .AddCookie("Cookies", options =>
                {
                    options.AccessDeniedPath = "/account/denied";
                    options.LoginPath = "/account/login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.SlidingExpiration = true;

                })
                .AddCAS(o =>
                {
                    o.CasServerUrlBase = Configuration["CasBaseUrl"];   // Set in `appsettings.json` file.
                    o.SignInScheme = "Cookies";
                    o.Events.OnCreatingTicket = async context => {
                        var identity = (ClaimsIdentity) context.Principal.Identity;
                        var assertion = context.Assertion;
                        if (identity == null)
                        {
                            return;
                        }

                        var kerb = assertion.PrincipalName;

                        if (string.IsNullOrWhiteSpace(kerb)) return;

                        var identityService = services.BuildServiceProvider().GetService<IIdentityService>();
                        

                        var user = await identityService.GetByKerberos(kerb);

                        if (user == null)
                        {
                            return;
                        }                        

                         var existingClaim = identity.FindFirst(ClaimTypes.Name);
                        if(existingClaim != null)
                        {
                            identity.RemoveClaim(identity.FindFirst(ClaimTypes.Name));
                        }   
                        identity.AddClaim(new Claim(ClaimTypes.Name, user.Id.ToString()));

                        identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
                        identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
                        identity.AddClaim(new Claim("name", user.FullName));
                        identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                        existingClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                        if(existingClaim != null)
                        {
                            identity.RemoveClaim(identity.FindFirst(ClaimTypes.NameIdentifier));
                        }
                        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, kerb));
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Employee"));                        

                        context.Principal.AddIdentity(identity);

                        await Task.FromResult(0); 
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Client_route",
                   areaName: "Client",
                   pattern:  "client/{controller}/{action=Index}/{id?}"
                );

                endpoints.MapAreaControllerRoute(
                    name: "Admin_route",
                   areaName: "Admin",
                   pattern:  "admin/{controller}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
