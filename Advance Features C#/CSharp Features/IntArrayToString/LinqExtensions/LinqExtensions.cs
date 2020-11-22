﻿using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static string ToStringNonLinq<T>(this T[] array, string delimiter)
        {
            if (array != null)
            {
                // edit: replaced my previous implementation to use StringBuilder
                if (array.Length > 0)
                {
                    StringBuilder builder = new StringBuilder();

                    builder.Append(array[0]);
                    for (int i = 1; i < array.Length; i++)
                    {
                        builder.Append(delimiter);
                        builder.Append(array[i]);
                    }

                    return builder.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return null;
            }
        }

        public static string ToStringNonLinq2<T>(this T[] array, string delimiter)
        {
            if (array != null)
            {
                // determine if the length of the array is greater than the performance threshold for using a stringbuilder
                // 10 is just an arbitrary threshold value I've chosen
                if (array.Length < 10)
                {
                    // assumption is that for arrays of less than 10 elements
                    // this code would be more efficient than a StringBuilder.
                    // Note: this is a crazy/pointless micro-optimization.  Don't do this.
                    string[] values = new string[array.Length];

                    for (int i = 0; i < values.Length; i++)
                        values[i] = array[i].ToString();

                    return string.Join(delimiter, values);
                }
                else
                {
                    // for arrays of length 10 or longer, use a StringBuilder
                    StringBuilder sb = new StringBuilder();

                    sb.Append(array[0]);
                    for (int i = 1; i < array.Length; i++)
                    {
                        sb.Append(delimiter);
                        sb.Append(array[i]);
                    }

                    return sb.ToString();
                }
            }
            else
            {
                return null;
            }
        }

        /// Last Version...
        public static string ToStringNonLinq3<T>(this IEnumerable<T> input, string delimiter)
        {
            return input.Delimit(delimiter);
        }

        // concatenate the strings in an enumeration separated by the specified delimiter
        public static string Delimit<T>(this IEnumerable<T> input, string delimiter)
        {
            IEnumerator<T> enumerator = input.GetEnumerator();

            if (enumerator.MoveNext())
            {
                StringBuilder builder = new StringBuilder();

                // start off with the first element
                builder.Append(enumerator.Current);

                // append the remaining elements separated by the delimiter
                while (enumerator.MoveNext())
                {
                    builder.Append(delimiter);
                    builder.Append(enumerator.Current);
                }

                return builder.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
