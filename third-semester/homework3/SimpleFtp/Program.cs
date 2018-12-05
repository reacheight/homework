using System;

namespace SimpleFtp
{
    public class Program
    {
        public static void PrintMenu()
        {
            Console.WriteLine("Commands:\n" +
                              "1 <directory_path> - list content of directory\n" +
                              "2 <file_path> <new_file_name> - download <file_path> to <new_file_name>" +
                              "help - list of commands" +
                              "exit - close application");
        }
        public static void Main(string[] args)
        {
            using (var server = new FtpServer(12345, 5))
            {
                server.Start();

                using (var client = new FtpClient("localhost", 12345))
                {
                    PrintMenu();
                    var command = string.Empty;
                    while (command != "exit")
                    {
                        Console.WriteLine("Enter command:");
                        command = Console.ReadLine();
                        if (command == "help")
                        {
                            PrintMenu();
                            continue;
                        }

                        var tokens = command.Split();
                        switch (tokens.Length)
                        {
                            case 2 when tokens[0] == "1":
                                Console.WriteLine(client.ListCommand(tokens[1]).Result);
                                break;
                            
                            case 3 when tokens[0] == "2":
                                try
                                {
                                    var size = client.GetCommand(tokens[1], tokens[2]).Result;
                                    if (size == -1)
                                    {
                                        Console.WriteLine("File is not found.");
                                        continue;
                                    }
                                    
                                    Console.WriteLine($"File downloaded, size: {size}");
                                }
                                catch (DownloadErrorException exception)
                                {
                                    Console.WriteLine(exception.Message);
                                }

                                break;
                            
                            default:
                                Console.WriteLine("Command is not found.");
                                break;
                        }
                    }
                }
            }
        }
        
    }
}