using AppCore.Data;
using AppCore.Interfaces;
using AppCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup
    {
        private IWebHostEnvironment _environment;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string strConnection = "";
            string strDbPath = _environment.ContentRootPath;
            strDbPath = strDbPath.Replace("WebAPI", "AppCore\\Data\\wooliesdb.mdf;");
            strConnection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + strDbPath + "Integrated Security=True";

            services.AddControllers();

            // register application dbcontext
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(strConnection));

            // register woolies repository
            services.AddScoped(typeof(IWooliesRepository<>), typeof(WooliesRepository<>));
            services.AddScoped<IWooliesDataService, WooliesDataService>();
            services.AddScoped<ITrollyService, TrollyService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Woolies API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Woolies API"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
