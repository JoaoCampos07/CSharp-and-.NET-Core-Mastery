
//https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/yield

namespace SimpleYieldExample
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var result = Power(2, 8);

            foreach (var i in result)
            {
                Console.WriteLine("{0}", i);
            }
        }

        private static IEnumerable<int> Power(int number, int exponent)
        {
            int result = 1;

            for (int i = 0; i < exponent; i++)
            {
                result = result * number;
                yield return result;
            }
        }
    }
}

// Each iteration in Foreach in Main() makes a call to the Power method

// What the yield word do ? 
// R: Indicates that the method where is used, is a Iterator. 
// This removes a necessity for a extra class (A class that holds the state of the enumeration.)

// Why we use the yield return statement ? 
// R: We use it to return one element at time.