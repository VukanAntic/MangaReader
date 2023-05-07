﻿using IdentityServer.DTOs;
using IdentityServer.Entities;
using Microsoft.OpenApi.Models;
using System.Reflection;
using AutoMapper;

namespace IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication();

            var mongoDbSettings = Configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            var mongoDbName = Configuration.GetValue<string>("DatabaseSettings:MongoName");
            services.AddIdentity<User, Role>()
                .AddMongoDbStores<User, Role, Guid>(
                mongoDbSettings, mongoDbName
                );


            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<UserCreateDTO, User>().ReverseMap();
            });

            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<RoleCreateDTO, Role>().ReverseMap();
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityServer", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityServer v1"));
            }

            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseAuthentication();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
