
using System;
using Business.Abstract;
using Business.Concrete;
using Castle.Core.Configuration;
using DataAccess.Concrete.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebUI.CustomValidator;
using WebUI.EmailServices;
using WebUI.Hubs;
using WebUI.TwoFactorServices;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace WebUI
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                services.AddDbContext<BookCycleContext>(options => options.UseSqlServer(_configuration.GetConnectionString("BookCycleContextProd")));
            else
                services.AddDbContext<BookCycleContext>(options => options.UseSqlServer(_configuration.GetConnectionString("BookCycleContext")));

            var context = services.BuildServiceProvider().GetService<BookCycleContext>();
            context.Database.Migrate();

            services.AddRazorPages()
                .AddRazorRuntimeCompilation();
          
            services.AddDbContext<BookCycleContext>();
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.SignIn.RequireConfirmedEmail = true;
            }).AddErrorDescriber<CustomIdentityValidator>().AddEntityFrameworkStores<BookCycleContext>().AddDefaultTokenProviders();
            services.AddSignalR();
       

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.IsEssential = true;
                opt.Cookie.Name = "BookCycleCookie";
                opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                opt.Cookie.HttpOnly = true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(20);
                opt.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
                opt.LoginPath = "/Home/Index";
            });

            services.AddSingleton<IEmailSender, SmtpEmailSender>();
            services.Configure<EmailOptions>(_configuration);

            services.Configure<TwoFactorOptions>(_configuration.GetSection("TwoFactorOptions"));
            services.AddScoped<TwoFactorService>();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.Name = "MainSession";
            });
            
            services.AddControllersWithViews();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager , RoleManager<AppRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
         
            app.UseAuthorization(); 
            IdentityInitializer.SeedData(userManager, roleManager).Wait();
            app.UseStaticFiles();

            app.UseSession();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapHub<ChatHub>("/Member/Chat");
            });
           

        }
    }
}
