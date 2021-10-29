using System;
using Microsoft.IC3.Distribution.Common.Enums;
using Microsoft.IC3.Distribution.Common.Model;
using Microsoft.IC3.Distribution.Publication.Exceptions;
using Microsoft.IC3.Distribution.Publication.Metrics;
using Microsoft.IC3.Distribution.Publication.Model;
using Microsoft.IC3.Distribution.Publication.Service;
using Microsoft.R9.Extensions.Metering;
using Microsoft.Skype.ServiceShared.Event.Correlation;
//using Moq;
//using Moq.Protected;
//using NUnit.Framework;
//using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace TestClient
{
    internal class Program
    {
        private static readonly object Data = new();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var p = new Program();
            p.GeneratorAsync().Wait();
        }

        private async Task GeneratorAsync()
        {
            var mockHttpMessageHandler = new HttpClientHandler();
            var httpClient = new HttpClient(mockHttpMessageHandler); // .

            var config = new PublisherConfiguration
            {
                CertificateThumbprint = "4c815e7731b51ac7cf8fd1c4d016ab7051ad7467",
                RunningEnvironment = RunningEnvironment.Int,
                Source = "MEC",
                SourceRegion = Region.NOAM,
            };
            var metrics = new Metrics(NullMeter.Instance);
            var monitor = new MetricsMonitor(metrics);
            IPublisher publisher = new Ic3PubSubPublisher(config, httpClient, monitor);
            var message = CreateBasicMessage();
            var result = await publisher.PublishAsync(message, CancellationToken.None);
            Console.WriteLine($"Response: {result}");
        }

        private DistributionMessage CreateBasicMessage()
        {
            return CreateBasicMessage(DistributionMessageType.None, 1U);
        }

#pragma warning disable CA1822 // Mark members as static
        private DistributionMessage CreateBasicMessage(
            DistributionMessageType type, uint dataversion)
        {
            var message = new DistributionMessage(Data)
            {
                Type = type,
                DataVersion = dataversion,
            };
            message.Metadata.ProductContext = "MEC";
            message.Metadata.EntityName = "UserUnionPolicies";
            return message;
        }
#pragma warning restore CA1822 // Mark members as static
    }
}
