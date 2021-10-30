using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;

namespace CoreTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hostsettings.json", optional: true)
                .AddCommandLine(args)
                .Build();

            IApplicationBuilder bld;

            return WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:5000")
                .UseConfiguration(config)
                .Configure(app =>
                {
                    app.Run(Handler);
                });
        }

        private static Task Handler(HttpContext context)
        {
            var req = context.Request;
            var payload = req.Body;

            //payload.Seek(0, SeekOrigin.Begin);

            using (StreamReader stream = new StreamReader(payload))
            {
                string uri = req.Path;
                string query = req.QueryString.ToString();
                string body = stream.ReadLineAsync().Result;
                Console.WriteLine($"Request: {body}");
            }

            context.Response.WriteAsync("Hello, World!");
            return Task.CompletedTask;
        }
    }
}
