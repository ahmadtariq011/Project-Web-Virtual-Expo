using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpo.Model.Data;
using VirtualExpo.Web.Hubs;

namespace VirtualExpo
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
            services.AddCors();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Admin";
            })
            .AddCookie("Admin", options =>
            {
                options.LoginPath = "/Home/Login/";
                options.AccessDeniedPath = "/Account/AccessDenied/";

            })
            .AddCookie("Organizer", options =>
            {
                options.LoginPath = "/Home/Login/";
                options.AccessDeniedPath = "/Account/AccessDenied/";
            })
            .AddCookie("Exhibitor", options =>
            {
                options.LoginPath = "/Home/Login/";
                options.AccessDeniedPath = "/Account/AccessDenied/";
            })
            .AddCookie("Attendee", options =>
            {
                options.LoginPath = "/Home/LoginAtenee/";
                options.AccessDeniedPath = "/Account/AccessDenied/";
            });
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("VirtualExpoDB")));
            services.AddControllersWithViews();
            services.AddSignalR();

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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };
            app.UseCookiePolicy(cookiePolicyOptions);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=HomeIndex}/{id?}");
                endpoints.MapHub<ChatHub>("/chathub");

            });
        }
    }
}
