namespace YieldKeyword
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            // The objective is too get the powerOfTwo of all the numbers in a int[] and print in the console.

            Example1();

            Example2();
        }

        #region Example1
        private static int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

        private static void Example1()
        {
            // without Yield...first operation i compute all the power of two and return an array.
            var numbers = ExponentOfTwo();

            for (int i = 0; i < numbers.Length; i++) //O(15) + O(15)
            {
                Console.WriteLine(numbers[i]); //second operation i print them
            }

            // using Yield..
            foreach (var item in ExponentOfTwo_withEnumerator()) // O(15)
            {
                Console.WriteLine(item);
            }
        }

        public static IEnumerable<int> ExponentOfTwo_withEnumerator()
        {
            foreach (var item in numbers)
            {
                yield return item * item; // we use yield method and we dont need to create that holds the state of the enumerator. In which position is at etc...
            }
        }

        public static int[] ExponentOfTwo()
        {
            var powerOfTwo = new int[numbers.Length];

            for (int i = 0; i < numbers.Length; i++)
            {
                powerOfTwo[i] = numbers[i] * numbers[i];
            }

            return powerOfTwo;
        } 
        #endregion

        #region Example2 
        private static void Example2()
        {
            var result = Power(2, 8); // first operation 

            foreach (var i in result)
            {
                Console.WriteLine("{0}", i); // second operation
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
        #endregion
    }
}
