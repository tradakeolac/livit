using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using Autofac;
using Livit.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Livit.Infrastructure.Initialization;
using Autofac.Extensions.DependencyInjection;
using Livit.Web.Infrastructure.DependencyInjection;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Livit.Web.Infrastructure.ErrorHandling;
using Swashbuckle.AspNetCore.Swagger;

namespace Livit.Web.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("client_secret.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        // ConfigureServices is where you register dependencies. This gets
        // called by the runtime before the Configure method, below.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add services to the collection.
            services.AddMvc()
                .AddControllersAsServices();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Livit Leave Management Api", Version = "v1" });
            });

            services.AddApiVersioning();

            services.AddDbContext<LivitDbContext>(options => options.UseSqlite("FileName=./livit.db"));

            // Create the container builder.
            var builder = new ContainerBuilder();

            // Register services
            builder.RegisterInstance(this.Configuration)
                .As<IConfiguration>()
                .SingleInstance();

            // Register dependencies, populate the services from
            // the collection, and build the container. If you want
            // to dispose of the container at the end of the app,
            // be sure to keep a reference to it as a property or field.
            ILivitServiceCollection autofacServices = new AutofacServiceCollection(builder);
            InitializationContext configurationContext = new AutofacInitializationContext(autofacServices);

            // Initialize modules
            InitializationProcessor.Initialize(configurationContext);

            builder.Populate(services);
            this.ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Add error handling
            app.UseContentNegotiateExceptionHandling();

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUi(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Livit Leave Management Api V1");
            });
        }
    }
}
