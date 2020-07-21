namespace BlazorWebUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using BlazorWebUI.Data;
    using DataAccessLibrary;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //WOUTER: ADD ADDITIONAL SERVICES HERE
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>(); //creates one instance for entire application
            services.AddTransient<ISqlDataAccess, SqlDataAccess>(); // creates an instance everty time we ask for one.
            services.AddTransient<IPeopleData, PeopleData>();

            
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

                //WOUTER: THIS IS FOR FUTURE LOADING IF ERROR AFTER 30 DAYS
                //PLEASE REFACTOR TO CORRECT OUTPUT AND SITE REFERENCE - DO RESEARCH 
                //services.AddHsts(options =>
                //{
                //    options.Preload = true;
                //    options.IncludeSubDomains = true;
                //    options.MaxAge = TimeSpan.FromDays(60);
                //    options.ExcludedHosts.Add("example.com");
                //    options.ExcludedHosts.Add("www.example.com");
                //});

            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
