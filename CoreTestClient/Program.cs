using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using CoreTestCommon;
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
            var cancellationTokenSource = new CancellationTokenSource();
            var mockHttpMessageHandler = new HttpClientHandler();
            var httpClient = new HttpClient(mockHttpMessageHandler);
            // TODO: Should be taken from Configuration
            var requestUri = "http://localhost:5000/api/CoreTest2";
            // var requestUri = "http://localhost:5000/local";
            // var requestUri = "http://localhost:5000";
            IDictionary<string, string> headersAsDictionary = new Dictionary<string, string>();

            headersAsDictionary.Add(X_MS_CORRELATION_ID, Guid.NewGuid().ToString());
            headersAsDictionary.Add("User-Agent", Agent);

            int numberOfRepetitions = 1;
            for (int i = 0; i < numberOfRepetitions; i++)
            {
                var httpRequestMessage = await BuildRequestMessage(requestUri, headersAsDictionary, iteration: i);
                await SendToCoreTest(httpClient, httpRequestMessage, cancellationTokenSource.Token); 
            }
        }
        
        private static async Task<HttpRequestMessage> BuildRequestMessage(string url, IDictionary<string, string> headersAsDictionary, int iteration)
        {
            await Task.Run(() => "none");
            var testPayload = new TestPayload { Name = "sergeim", Age = 100 + iteration, };
            var jsonMsg = JsonConvert.SerializeObject(testPayload);
            var stringContent = new StringContent(jsonMsg, System.Text.Encoding.UTF8, "application/json");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            foreach (var headerEntry in headersAsDictionary)
            {
                httpRequestMessage.Headers.Add(headerEntry.Key, headerEntry.Value);
            }
            httpRequestMessage.Content = stringContent;
            return httpRequestMessage;
        }

        private async Task SendToCoreTest(HttpClient httpClient, HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            try
            {
                var response = await httpClient.SendAsync(
                    httpRequestMessage,
                    cancellationToken).ConfigureAwait(false);

                var success = response.IsSuccessStatusCode;

                if (!success)
                {
                    var errorMessage = $"Failed. " +
                        $"Message failed with the following Status Code '{response.StatusCode}'";
                    Console.WriteLine(errorMessage);
                }

                if (response.Content != null)
                {
                    var payload = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response payload: {payload}");
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
}
