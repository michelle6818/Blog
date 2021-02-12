using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Models;
using Blog.Services;
using Blog.Utilites;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blog
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    DataUtility.GetConnectionString(Configuration)));
            services.AddDatabaseDeveloperPageExceptionFilter();



            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddIdentity<BlogUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultUI() //may not absolutely need
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();
            //This is how I register a custom class as a service
            services.AddTransient<ISlugService, BasicSlugService>();

            //Register our new BasicImageService
            services.AddTransient<IImageService, BasicImageService>();

            //github
            services.AddAuthentication()
                .AddGitHub(options =>
                {
                    options.ClientId = "6682fe182ad27ab21e7e";
                    options.ClientSecret = "0f3b22ee06dc50230adafecb2ce38751f76128b6";
                    options.AccessDeniedPath = "/AccessDeniedPathInfo";
                });
            //facebook
            services.AddAuthentication()
                .AddFacebook(options =>
                {
                    options.AppId = "204427018038372";
                    options.AppSecret = "a85cb6ff663b95f1ececd8f15be76364";
                    options.AccessDeniedPath = "/AccessDeniedPathInfo";
                });
            //twitter
            services.AddAuthentication()
                .AddTwitter(options =>
                {
                    options.ConsumerKey = "ibCCJpqOLKwNJDc5BdWeuByBW";
                    options.ConsumerSecret = "SCBGAsyDZ1zE8NZI9r3kGHXdJ0KCtggb60LFFdaZ5NftQzNQyJ";
                    options.RetrieveUserDetails = true;
                    options.AccessDeniedPath = "/AccessDeniedPathInfo";
                });
            //google
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "227791669845-4cggpk919e67oaoauaoqiqpihtc3mile.apps.googleusercontent.com";
                    options.ClientSecret = "ZI_ZjdSYPkfGoCxtSd5eESbR";
                    options.AccessDeniedPath = "/AccessDeniedPathInfo";
                });

            //Services needed to send emails
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddScoped<IEmailSender, EmailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            app.UseEndpoints(endpoints =>
            {
                //UNCOMMENT THIS FOR SLUGS
                endpoints.MapControllerRoute(
                    name: "slugRoute",
                    pattern: "Posts/TheDetails/{slug}",
                    defaults: new { controller = "CategoryPosts", action = "Details" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
