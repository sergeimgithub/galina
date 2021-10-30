using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CoreTestClient
{
    internal class Program
    {
        public static readonly string X_MS_CORRELATION_ID = "X-MS-Correlation-Id";
        public static readonly string Agent = "CoreTestClient";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var p = new Program();
            p.Run(args).Wait();
        }

        private async Task Run(string[] args)
        {
            var cancellationToken = new CancellationTokenSource();
            var mockHttpMessageHandler = new HttpClientHandler();
            var httpClient = new HttpClient(mockHttpMessageHandler); // .
            // TODO: Should be taken from Configuration
            var url = "http://localhost:5000";
            var testPayload = new TestPayload { Name = "sergeim", Age = 100, };
            var jsonMsg = JsonConvert.SerializeObject(testPayload);
            var stringContent = new StringContent(jsonMsg, System.Text.Encoding.UTF8, "application/json");

            IDictionary<string, string> hdrs = new Dictionary<string, string>();

            hdrs.Add(X_MS_CORRELATION_ID, Guid.NewGuid().ToString());
            hdrs.Add("User-Agent", Agent);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            foreach (var headerEntry in hdrs)
            {
                httpRequestMessage.Headers.Add(headerEntry.Key, headerEntry.Value);
            }
            httpRequestMessage.Content = stringContent;

            try
            {
                var response = await httpClient.SendAsync(
                    httpRequestMessage,
                    cancellationToken.Token).ConfigureAwait(false);

                var success = response.IsSuccessStatusCode;

                if (!success)
                {
                    var errorMessage = $"Failed. " +
                        $"Message failed with the following Status Code '{response.StatusCode}'";
                    Console.WriteLine(errorMessage);

                    if (response.Content != null)
                    {
                        var payload = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Response payload: {payload}");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                var errorMessage = $"Publish - Message could not be sent due to: '{ex.Message}'";
                HandleException(ex, errorMessage);
            }
            catch (TimeoutException ex)
            {
                var errorMessage = $"Publish - Message timed out due to: '{ex.Message}'";
                HandleException(ex, errorMessage);
            }
            catch (TaskCanceledException ex)
            {
                var errorMessage = $"Publish - Message got cancelled due to: '{ex.Message}'";
                HandleException(ex, errorMessage);
            }
        }

        private void HandleException(
            Exception exception,
            string errorMessage)
        {
            Console.WriteLine(errorMessage, exception);
        }

    }

    internal class TestPayload
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
