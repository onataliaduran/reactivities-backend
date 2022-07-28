using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using Persistence;
using Application.Activities;
using Application.Core;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });

            // Inject the DataContext class in the services and specify the data server we are using
            services.AddDbContext<DataContext>(opt => 
            {
              opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            // Cors policy
            services.AddCors(opt => {
              opt.AddPolicy("CorsPolicy", policy =>
              {
                policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000"); 
              });
            });

            // Mediator
            services.AddMediatR(typeof(List.Handler).Assembly); // This tells Mediator where to find our handlers

            // Mapper
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}