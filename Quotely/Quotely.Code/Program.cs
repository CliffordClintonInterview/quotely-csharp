using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using Quotely.Code.Handlers;
using Quotely.Code.Services;

namespace Quotely.Code
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                // Add Host
                var host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        services.AddHttpClient("Forismatic", client =>
                        {
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        });
                        services.RemoveAll<IHttpMessageHandlerBuilderFilter>();
                        services.AddTransient<IArgHandler, ArgHandler>();
                        services.AddTransient<IQuoteService, QuoteService>();
                    })
                    .Build();

                var argHandler = ActivatorUtilities.CreateInstance<ArgHandler>(host.Services);
                var res = await argHandler.ParseArgumentAsync(args);
                Console.WriteLine(res);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
