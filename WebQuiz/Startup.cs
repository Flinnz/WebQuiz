using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using React.AspNet;
using Swashbuckle.AspNetCore.Swagger;
using WebQuiz.Domain;

namespace WebQuiz
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
         services
             .AddMvc()
             .AddJsonOptions(options =>
             {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Populate;
             })
             .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
         services.AddSingleton<IQuestionsRepository>(qr => new InMemoryQuestionsRepository());

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
            app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
            {
               HotModuleReplacement = true,
               ReactHotModuleReplacement = true
            });
            app.UseDeveloperExceptionPage();
         }
         else
         {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
         }
         app.UseStaticFiles();
         app.UseHttpsRedirection();
         app.UseSwagger();
         app.UseSwaggerUI(c =>
         {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebQuiz v1");
         });
         app.UseMvc();
         app.UseSpa(spa =>
         {
            spa.Options.SourcePath = "App";
         });
      }
   }
}
