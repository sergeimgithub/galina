using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

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
                .AddJsonFile("appsettings.json", optional: true)
                .AddCommandLine(args)
                .Build();

            // Get configuration way 1
            var positionOptions = new PositionOptions();
            config.GetSection(PositionOptions.Position).Bind(positionOptions);
            Console.WriteLine($"Title: {positionOptions.Title}");
            Console.WriteLine($"Name: {positionOptions.Name}");

            var requestUri01 = "http://localhost:5000";
            var requestUri02 = "https://coretest20211031021946.azurewebsites.net";
            var requestUri03 = "https://*";
            var requestUri04 = "http://localhost:5000";

            var requestUri = requestUri03;


            var host = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
//                .UseUrls(requestUri)
//                .UseUrls("http://localhost:5000")
//                .UseUrls("https://coretest20211031021946.azurewebsites.net")
                .UseConfiguration(config)
#if false
        .Configure(app =>
                {
                    app.UsePathBase("/local");
                    app.Run(Handler);
                }) 
#endif
                ;

            return host;
        }

        internal static Task Handler(HttpContext context)
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
