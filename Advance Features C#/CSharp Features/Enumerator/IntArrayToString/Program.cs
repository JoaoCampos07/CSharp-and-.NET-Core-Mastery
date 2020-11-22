/*
 * https://stackoverflow.com/questions/1822811/int-array-to-string
 */

namespace IntArrayToString
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            // I have a array of ints, containing digits only. I want to convert this array to string.
            // How to do it ?

            int[] arr = { 0, 1, 2, 3, 0, 1 };

            FirstOption_Join(arr);

            SecondOption_AggregateFunc(arr);

            ThirdOption_ExtensionMethod(arr);

            var enumerator = Enumerable.Range(0, 5).ToList().GetEnumerator();

            while(enumerator.MoveNext())
            {
                Console.WriteLine($"{enumerator.Current}");
            }
        }

        private static void ThirdOption_ExtensionMethod(int[] arr)
        {
            var result = arr.ToStringNonLinq3<int>("");
            Console.WriteLine($" Result : {result}");
        }

        private static void SecondOption_AggregateFunc(int[] arr)
        {
            var result = arr.Aggregate(string.Empty, (s, i) => s + i.ToString());

            Console.WriteLine($" Result : {result}");
        }

        private static void FirstOption_Join(int[] arr)
        {
            // with Linq
            // 1. Convert to List<int> to easy manipulate 
            // 2. Convert every element of this list to string
            // 3. Convert again to array.
            // 4. Join each element of this array to 
            var list = new List<int>(arr);
            var strlist = list.ConvertAll(i => i.ToString());
            var finalArray = strlist.ToArray();

            string result = string.Join("", finalArray);

            Console.WriteLine($" Result : {result}");
        }
    }
}
