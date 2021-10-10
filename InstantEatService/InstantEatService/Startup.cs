using InstantEatService.Repositories;
using InstantEatService.Services;
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

namespace InstantEatService
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
            var connection = Configuration.GetConnectionString("Sqlite");
            //services.AddDbContext<InstantEatDbContext>(options =>
            //    options.UseSqlServer(connection));

            services.AddDbContext<InstantEatDbContext>(options =>
                options.UseSqlite(connection));

            services.AddScoped<IClientsRepository, ClientsInMsSqlRepository>();
            services.AddScoped<IFoodItemsRepository, FoodItemsInMsSqlRepository>();
            services.AddScoped<IFoodItemsService, FoodItemsService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IJsonDataService, InstantEatJsonDataService>();
            services.AddScoped<IBusinessLunch, BusinessLunchService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InstantEatService", Version = "v1" });
                c.IncludeXmlComments("InstantEatService.xml", true);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InstantEatService v1"));
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
