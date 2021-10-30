using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreTest
{
    public class PositionOptions
    {
        public const string Position = "Position";

        public string Title { get; set; }
        public string Name { get; set; }
    }

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Get configuration way 2
            var positionOptions = new PositionOptions();
            Configuration.GetSection(PositionOptions.Position).Bind(positionOptions);
            Console.WriteLine($"Title: {positionOptions.Title}");
            Console.WriteLine($"Name: {positionOptions.Name}");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<PositionOptions>(Configuration.GetSection(PositionOptions.Position));
#if false
            // https://eng.ms/docs/experiences-devices/r9-sdk/docs/telemetry/metering/geneva-metric-export
            services.AddGenevaMetering(Configuration.GetSection("GenevaMetering"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Distribution.TestService", Version = "v1" });
            }); 
#endif
        }

        public void Configure(IApplicationBuilder app, IHostApplicationLifetime appLifetime, IWebHostEnvironment env)
        {
            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                appLifetime.StopApplication();
                // Don't terminate the process immediately, wait for the Main thread to exit gracefully.
                eventArgs.Cancel = true;
            };

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

#if false
            app.UsePathBase("/local"); 
            app.Run(Program.Handler);
#endif
        }

        private void OnStarted()
        {
            // Perform post-startup activities here
        }

        private void OnStopping()
        {
            // Perform on-stopping activities here
        }

        private void OnStopped()
        {
            // Perform post-stopped activities here
        }
    }
}
