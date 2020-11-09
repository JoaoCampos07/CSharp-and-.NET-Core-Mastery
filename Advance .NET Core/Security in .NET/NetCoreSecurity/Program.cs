using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace IDisposableTraining
{
    class Program
    {
        private static ConsoleKey UserDecision { get; set; }

        static void Main(string[] args)
        {
            #region Configure DI and Logging
            Console.WriteLine("Program Started...");
            // Setup our DI ... equal to ConfigureServices() ... we got our container to resolve Depend.
            var serviceProvider = new ServiceCollection()
                .AddLogging(opt =>
               {
                   opt.AddConsole();
               })
                .AddSingleton<IFooService, FooService>() // se eu as minhas class nao tiverem a interface que estou a por aqui associada : There is no implicit reference conversion 
                .AddSingleton<IBarService, BarService>() // between FooService & IFooService... Ou seja a interface nao esta associada a classe.
                .BuildServiceProvider();

            // Configure console Logging ...
            var logger = serviceProvider
                    .GetService<ILoggerFactory>();
            ILogger<Program> loggerPrg = logger.CreateLogger<Program>();
            loggerPrg.LogInformation("We are live !");

            #endregion

            Console.WriteLine("Let's play with X \n");

        MaisUmaVoltinha:

            Console.WriteLine("Press 0 : Do someting \n");

            var input = Console.ReadKey();

            switch (input.Key) //Switch on Key enum
            {
                case ConsoleKey.NumPad0:
                case ConsoleKey.D0:
                    SomeMethod();
                    break;
            }

            CheckIfUserWantsToGoBack();

            if (Program.UserDecision == ConsoleKey.NumPad1 || Program.UserDecision == ConsoleKey.D1)
            {
                goto MaisUmaVoltinha;
            }
        }

        private static void SomeMethod()
        {
            throw new NotImplementedException();
        }

        #region Program Utils
        public static void CheckIfUserWantsToGoBack()
        {
            Console.WriteLine("Do you want to go back to menu ? \n");
            Console.WriteLine("Press 1 : to go back to menu");
            Console.WriteLine("Press 0 : press 0 to exit \n");

            var input = Console.ReadKey();

            Program.UserDecision = input.Key;
        }
        #endregion
    }
}
