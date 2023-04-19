using BookStore.DbOperations;
using BookStore.Middlewares;
using BookStore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BookStore
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
            services.AddControllers();

            //Swagger integration;
            services.AddSwaggerDocument(config => {
                //Extras;
                config.PostProcess = (doc =>
                {
                    doc.Info.Title = "Books Api";
                    doc.Info.Version = "1.0.13";
                });
            });

            services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDb"));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton<ILoggerService, ConsoleLogger>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //Swagger integration;
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseAuthorization();

            app.UseCustomException();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
