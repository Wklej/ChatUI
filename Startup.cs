using ChatUI.DB;
using ChatUI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace ChatUI
{
    public class Startup
    {
        private IConfiguration _config;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration conifg)
        {
            _config = conifg;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {

            var connectionString = _config.GetConnectionString("DefaultConnection");

            services.AddMvc(options => options.EnableEndpointRouting = false);   
            
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(connectionString)
            );
            
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();


            app.UseMvcWithDefaultRoute();
        }
    }
}
