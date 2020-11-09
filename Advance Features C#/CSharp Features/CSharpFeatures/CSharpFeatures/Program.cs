using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace CSharpFeatures
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Program for assessment 1 was started...");

            // Setup our DI ... equal to ConfigureServices() ... we got our container to resolve DI.
            var serviceProvider = new ServiceCollection()
                .AddLogging(opt =>
               {
                   opt.AddConsole();
               })
                .AddSingleton<IFooService, FooService>()
                .BuildServiceProvider();

            // Configure console Logging ...
            var logger = serviceProvider
                    .GetService<ILoggerFactory>();
            ILogger<Program> loggerPrg = logger.CreateLogger<Program>();
            loggerPrg.LogInformation("We are live !");

            //do the actual work here
            var foo = serviceProvider.GetService<IFooService>();
            foo.DoWork();
        }
    }
}
