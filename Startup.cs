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
using Microsoft.OpenApi.Models;

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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Michelle's Dev Blog",
                    Version = "v1",
                    Description = "Michelle's Development Blog",
                    Contact = new OpenApiContact
                    {
                        Name = "Michelle Longworth",
                        Email = "mlongworth@alumni.unc.edu",
                        Url = new Uri("https://michellelongworth-portfolio.netlify.app")
                    }
                });
            });
            //github
            services.AddAuthentication()
                .AddGitHub(options =>
                {
                    options.ClientId = "046c55c873ab73500326";
                    options.ClientSecret = "5647436dd72e85025435e20629020d10af360b56";
                    options.AccessDeniedPath = "/AccessDeniedPathInfo";
                });
            //facebook
            services.AddAuthentication()
                .AddFacebook(options =>
                {
                    options.AppId = "513804523343027";
                    options.AppSecret = "24760b675debcc02f0cba8517f7c6b1c";
                    options.AccessDeniedPath = "/AccessDeniedPathInfo";
                });
            //twitter
            services.AddAuthentication()
                .AddTwitter(options =>
                {
                    options.ConsumerKey = "isCAfIz7Dm0IbeU40ZgrHNkq1";
                    options.ConsumerSecret = "tROc9JTz27BLNcVUqwTSUglhSsubaMV54u3gCj8kmAYI7aWfhQ";
                    options.RetrieveUserDetails = true;
                    options.AccessDeniedPath = "/AccessDeniedPathInfo";
                });
            //google
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "972171024296-omo8nlnmkjm192g7f08g57bh3kjqtn70.apps.googleusercontent.com";
                    options.ClientSecret = "8zrFx1q-JoUhi74xGgTcGHG1";
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Michelle's Dev Blog");
                c.InjectJavascript("/swagger/swagger.js");
                c.InjectStylesheet("/swagger/swagger.css");
                c.DocumentTitle = "Michelle's Dev Blog";
            });

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

                endpoints.MapControllers();
            });
        }
    }
}
