namespace YieldKeyword.UserCases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TwoSimpleOperations
    {
        public void DisplayPowerOfTwo()
        {
            // TOTAL : O(n^2)
            // ...first operation i compute all the power of two
            var numbers = PowerWithoutYield(2, 8).ToArray(); // O(n)

            for (int i = 0; i < numbers.Length; i++) // O(n) 
            {
                Console.WriteLine(numbers[i]); //second operation i print them
            }


            // with Yield and Lazy Enumerator => TOTAL : O(n) with Single Responsability being respected.
            foreach (var i in Power(2, 8))
            {
                Console.WriteLine("{0}", i); // second operation
            }
        }

        public static IEnumerable<int> PowerWithoutYield(int number, int exponent)
        {
            var powerOfTwo = new int[numbers.Length];

            for (int i = 0; i < numbers.Length; i++)
            {
                powerOfTwo[i] = numbers[i] * numbers[i];
            }

            return powerOfTwo;
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
