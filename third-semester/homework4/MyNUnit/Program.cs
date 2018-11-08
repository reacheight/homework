using System;
using System.IO;

namespace MyNUnit
{
    /// <summary>
    /// Class that implements command-line test application
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main function
        /// </summary>
        static void Main()
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
            catch (ArgumentException)
            {
                Console.WriteLine("Error occured: Path contains invalid characters.");
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
        }
    }
}