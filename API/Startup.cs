using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using Application.Activities;
using Application.Core;
using Persistence;
using API.Extensions;

namespace API
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

            services.AddControllers();
            services.AddApplicationServices(Configuration);
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            // });

            // // Inject the DataContext class in the services and specify the data server we are using
            // services.AddDbContext<DataContext>(opt => 
            // {
            //   opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            // });

            // // Cors policy
            // services.AddCors(opt => {
            //   opt.AddPolicy("CorsPolicy", policy =>
            //   {
            //     policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000"); 
            //   });
            // });

            // // Mediator
            // services.AddMediatR(typeof(List.Handler).Assembly); // This tells Mediator where to find our handlers

            // // Mapper
            // services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
    
}
