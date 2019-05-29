using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Server.Domain;
using Server.Hubs;
using MongoDB.Driver;

namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string CorsPolicy = "CorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Populate;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy, builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins("https://localhost:5001", "https://localhost:9000");
                        
                });
            });
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("WebQuizDB");
            
            services.AddSingleton<IQuestionsRepository>(qr => new MongoQuestionsRepository(database));
            services.AddSingleton<IGameRepository>(gamerep => new MongoGameRepository(database));
            services.AddSingleton<Dictionary<string, PlayerEntity>>(p => new Dictionary<string, PlayerEntity>());
            services.AddSignalR();
            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "WebQuiz API",
                    Version = "v1",
                    Description = "Cool and edgy documentation for WebQuiz API"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(CorsPolicy);
            app.UseSignalR(routes => 
            {
                routes.MapHub<QuizHub>("/multiplayer");
            });
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            { 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebQuiz v1"); 
                c.RoutePrefix = "";
            });
            app.UseMvc();            
        }
    }
}