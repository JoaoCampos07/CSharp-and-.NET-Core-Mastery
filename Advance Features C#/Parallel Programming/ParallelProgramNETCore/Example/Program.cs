using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace IDisposableTraining
{
    class Program
    {
        public static ConsoleKey UserDecision { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Let's play with X \n");

        MaisUmaVoltinha:

            Console.WriteLine("\t Press 0 : Calculations without Async and Await (1 Thread Blocked )\n");
            Console.WriteLine("\t Press 1 : Calculations using Async (3 Threads )\n");
            Console.WriteLine("\t Press 2 : Calculations WITH Async and Await Keywords (2 Threads )\n");
            Console.WriteLine("\t Press 3 : Calculations WITH Async and Await Keywords Waiting after something (2 Thread) \n");

            var input = Console.ReadKey();
            Console.WriteLine();
            Console.Clear();

            Console.WriteLine($"Main Thread {Thread.CurrentThread.ManagedThreadId} is running");
            Console.WriteLine($"Main Task {(Task.CurrentId == null ? "(dont have Id)" : Task.CurrentId.ToString()) } is running");

            switch (input.Key)
            {
                case ConsoleKey.NumPad0:
                case ConsoleKey.D0:
                    CalculateWithoutAsync();
                    break;
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    CalculateWithAsync();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    CalculateWithAsyncSimplify();
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    CalculateWithAsyncSimplify2();
                    break;
            }

            CheckIfUserWantsToGoBack();

            if (Program.UserDecision == ConsoleKey.NumPad1 || Program.UserDecision == ConsoleKey.D1)
            {
                goto MaisUmaVoltinha;
            }
        }

        // Here we use 1 Thread, the UI/Main Thread...
        private static void CalculateWithoutAsync()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running");

            int n = CalculateValue();
            Console.WriteLine("Only after calculation we could do other stuff.");
            Console.WriteLine($"The calculated value is : {n}");
        }

        // Here we use 3 Threads : 
        // 1) UI/Main Thread :  
        // 2) Thread to do the Calculation
        // 3) Thread to continue with Calculation
        private static void CalculateWithAsync()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running");

            Task<int> n = CalculateValueAsync();
            Console.WriteLine("Do other stuff while is calculating...");
            n.ContinueWith(t =>
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running in CallBack ContinueWith()");
                Console.WriteLine($"The calculated value is : {t.Result}");
            });

        }

        // Repare-se que eu liberto a Current Thread e ela contiua por ali fora para o menu... 
        // Mas as 3 linhas a seguir nao correm, fazem parte do meu callback
        // Here we use 2 Threads because we only release the Main Thread in the last Await
        private static async void CalculateWithAsyncSimplify()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running on CalculateWithAsyncSimplify()");

            int n = await CalculateValueAsyncSimplify();
            Console.WriteLine("Only after calculation we could do other stuff.");
            Console.WriteLine($"The calculated value is : {n}");
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running on CalculateWithAsyncSimplify() Callback");
        }

        // Neste caso eu uso o meu callback mais tarde, eu so "espero" pela Completion
        // quando preciso de usar o result de CalculateValueAsyncSimplify() 
        // e assim consigo fazer "Other stuff" enquanto espero
        // Aqui so uso 2 Threads. Main/Thread UI and Continuation Thread
        private static async void CalculateWithAsyncSimplify2()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running on CalculateWithAsyncSimplify2()");

            var n = CalculateValueAsyncSimplify();
            Console.WriteLine("Do other stuff while is calculating...");

            Console.WriteLine($"The calculated value is : {await n}"); // only here I release the Main thread 
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running on CalculateWithAsyncSimplify2() Callback");
        }

        #region Helpers 

        public static int CalculateValue()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running");

            Console.WriteLine("Calculating...");
            Thread.Sleep(5000);
            Console.WriteLine("Finish calculating.");
            return 123;
        }

        public static Task<int> CalculateValueAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running on CalculateValueAsync()");

                Console.WriteLine("Calculating...");
                Thread.Sleep(5000);
                Console.WriteLine("Finish calculating.");
                return 123;
            });
        }

        public static async Task<int> CalculateValueAsyncSimplify()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running on CalculateValueAsyncSimplify()");
            await Task.Delay(5000); // release the main thread 
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running on CalculateValueAsyncSimplify() Callback");
            return 123;
        }

        #endregion

        #region Program Utils

        public static void CheckIfUserWantsToGoBack()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} is running on CheckIfUserWantsToGoBack()");

            Console.WriteLine("Do you want to go back to menu ? \n");
            Console.WriteLine("Press 1 : to go back to menu");
            Console.WriteLine("Press 0 : press 0 to exit \n");

            var input = Console.ReadKey();

            Program.UserDecision = input.Key;
        }

        #endregion
    }
}
