using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.Bootstrapper;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.Infrastructure.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopManagement.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace ServiceHost
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
            services.AddHttpContextAccessor();
            var connectionString = Configuration.GetConnectionString("LampshadeConnection");
            ShopManagementBootstrapper.Configure(services, connectionString);
            DiscountManagementBootstrapper.Configure(services, connectionString);
            InventoryManagementBootstrapper.Configure(services, connectionString);
            BlogManagementBootstrapper.Configure(services, connectionString);
            CommentManagementBootstrapper.Configure(services, connectionString);
            AccountManagementBootstrapper.Configure(services, connectionString);

            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

            services.AddTransient<IFileUploader, FileUploader>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IAuthHelper, AuthHelper>();

            #region Authentication
            services.Configure<CookiePolicyOptions>(options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.Strict;
                });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            {
                o.LoginPath = new PathString("/Login");
                o.LogoutPath = new PathString("/Login");
                o.AccessDeniedPath = new PathString("/AccessDenied");
            });
            #endregion

            #region Authorization
            services.AddAuthorization(configure =>
            {
                configure.AddPolicy("AdminAreaAuthPolicy", configurePolicy =>
                {
                    configurePolicy.RequireRole(new List<string> { Roles.Administrator, Roles.SellManager, Roles.ContentManager });
                });

                configure.AddPolicy("InventoryManagementAuthPolicy", configurePolicy =>
                {
                    configurePolicy.RequireRole(new List<string> { Roles.Administrator, Roles.SellManager });
                });

                configure.AddPolicy("ShopManagementAuthPolicy", configurePolicy =>
                {
                    configurePolicy.RequireRole(new List<string> { Roles.Administrator, Roles.SellManager });
                });

                configure.AddPolicy("DiscountManagementAuthPolicy", configurePolicy =>
                {
                    configurePolicy.RequireRole(new List<string> { Roles.Administrator, Roles.SellManager });
                });

                configure.AddPolicy("AccountsManagementAuthPolicy", configurePolicy =>
                {
                    configurePolicy.RequireRole(new List<string> { Roles.Administrator });
                });

                configure.AddPolicy("BlogManagementAuthPolicy", configurePolicy =>
                {
                    configurePolicy.RequireRole(new List<string> { Roles.Administrator, Roles.ContentManager });
                });

                configure.AddPolicy("CommentsManagementAuthPolicy", configurePolicy =>
                {
                    configurePolicy.RequireRole(new List<string> { Roles.Administrator, Roles.ContentManager });
                });
            });
            #endregion

            services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminAreaAuthPolicy");
                    options.Conventions.AuthorizeAreaFolder("Administration", "/Inventory", "InventoryManagementAuthPolicy");
                    options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "ShopManagementAuthPolicy");
                    options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "DiscountManagementAuthPolicy");
                    options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "AccountsManagementAuthPolicy");
                    options.Conventions.AuthorizeAreaFolder("Administration", "/Blog", "BlogManagementAuthPolicy");
                    options.Conventions.AuthorizeAreaFolder("Administration", "/Comments", "CommentsManagementAuthPolicy");
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
