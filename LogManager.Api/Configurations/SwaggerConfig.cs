﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace LogManager.Api.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Log Manager",
                        Version = "v1",
                        Description = "Asp.Net Core WebAPI developed to manage applications log.",
                        Contact = new OpenApiContact
                        {
                            Name = "Rafael Brugiolo Souza",
                            Url = new Uri("https://www.linkedin.com/in/rafael-brugiolo/")
                        }
                    });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Avaliador de Idéias");
            });

            return app;
        }
    }
}
