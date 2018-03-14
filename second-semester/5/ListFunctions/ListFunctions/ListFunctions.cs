namespace ListFunctions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class for functions for list
    /// </summary>
    public class ListFunctions
    {
        /// <summary>
        /// Applies some function to all items in a list
        /// </summary>
        /// <typeparam name="TIn">Type of given list's elements</typeparam>
        /// <typeparam name="TOut">Type of given function's returned value</typeparam>
        /// <param name="list">Given list</param>
        /// <param name="function">Function, that applies to all items in a list</param>
        /// <returns>List obtained by applying the given function to each element of the given list</returns>
        public static List<TOut> Map<TIn, TOut>(List<TIn> list, Func<TIn, TOut> function)
        {
            var resultList = new List<TOut>();
            foreach (var item in list)
            {
                resultList.Add(function(item));
            }

            return resultList;
        }

        /// <summary>
        /// Returns a new list composed of elements of given list
        /// for which the value of given predicate is true
        /// </summary>
        /// <typeparam name="T">Type of given list's elements</typeparam>
        /// <param name="list">Given list</param>
        /// <param name="predicate">Given predicate</param>
        /// <returns>List composed of elements of given list for which the value of given predicate is true</returns>
        public static List<T> Filter<T>(List<T> list, Predicate<T> predicate)
        {
            var resultList = new List<T>();
            foreach (var item in list)
            {
                if (predicate(item))
                {
                    resultList.Add(item);
                }
            }

            return resultList;
        }

        /// <summary>
        /// Applies a given function to each element of a given list,
        /// returns the accumulated value after the whole pass of the list
        /// </summary>
        /// <typeparam name="TIn">Type of given list's elements</typeparam>
        /// <typeparam name="TOut">Type of given function's returned value</typeparam>
        /// <param name="list">Given list</param>
        /// <param name="startedValue">Started value for given function</param>
        /// <param name="function">Given function</param>
        /// <returns>Accumulated value after the whole pass of the list</returns>
        public static TOut Fold<TIn, TOut>(List<TIn> list, TOut startedValue, Func<TOut, TIn, TOut> function)
        {
            TOut result = startedValue;
            foreach (var item in list)
            {
                result = function(result, item);
            }

            return result;
        }
    }
}
