using System;
using System.IO;

namespace MyNUnit
{
    /// <summary>
    /// Class that implements command-line test application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main function
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("Enter full or relative path:");
            var path = Console.ReadLine();
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine(" Error occured: Entered path is empty");
            }

            try
            {
                Console.WriteLine("Test execution started.\n");
                TestSystem.RunTests(path);
                Console.WriteLine("Test execution finished.\n");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error occured: You don't have required permission.");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Error occured: Path is not found or is invalid.");
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("Error occured: Path too long.");
            }
            catch (IOException)
            {
                Console.WriteLine("Error occured: Path is a file name or network error has occured.");
            }
            catch (AggregateException exception)
            {
                var innermost = exception.InnerException.InnerException;
                if (innermost != null &&
                    (innermost is InvalidConstructorException || innermost is InvalidTestMethodException))
                {
                    Console.WriteLine($"Error occured: {innermost.Message}");
                }
                else
                {
                    throw;
                }
            }
        }
    }
}