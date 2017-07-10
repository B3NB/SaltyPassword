using System;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Extensions.CommandLineUtils;

namespace HashAndSalt
{
    class Program
    {

        static void Main(string[] args)
        {
#if DEBUG
            Console.WriteLine("DEBUG MODE");
#endif

            var app = new CommandLineApplication
            {
                Name = "Hash and Salt",
                Description = "Check if you remember your new password correctly."
            };
            app.HelpOption("-?|-h|--help");

            app.Command("make", (command) =>
            {
                command.Description = "Make a hash file";
                command.HelpOption("-?|-h|--help");

                var fileArgument = command.Argument("[file]", "Where the password hash will be saved.");

                command.OnExecute(() =>
                {
                    if (fileArgument.Value != null)
                    {
                        Make(fileArgument.Value);
                        return 0;
                    } else return 0;
                });
                 
            });

            app.Command("test", (command) =>
            {
                command.Description = "Test your password";
                command.HelpOption("-?|-h|--help");

                var fileArgument = command.Argument("[file]", "Where the password hash was saved.");

                command.OnExecute(() =>
                {
                    if (fileArgument.Value != null)
                    {
                        Test(fileArgument.Value);
                        return 0;
                    }
                    else return 0;
                });
            });

            app.OnExecute(() => 0);
            app.Execute(args);

#if DEBUG
            Console.ReadKey();
#endif
        }

        private static void Make(string path)
        {
            if (File.Exists(path))
            {
                Console.WriteLine("File already exists");
                return;
            }

            Console.WriteLine("Please type your password");
            string password = ReadPassword();
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[20];
                rng.GetBytes(salt);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
                {
                    byte[] hash = pbkdf2.GetBytes(40);

                    byte[] hashBytes = new byte[60];
                    Array.Copy(salt, 0, hashBytes, 0, salt.Length);
                    Array.Copy(hash, 0, hashBytes, salt.Length, hash.Length);

                    using (var file = File.Create(path))
                    {
                        file.Write(hashBytes, 0, hashBytes.Length);
                    }
                }
            }
        }

        private static void Test(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File doesn't exists");
                return;
            }

            byte[] hashBytes = File.ReadAllBytes(path);

            if (hashBytes.Length != 60)
            {
                Console.WriteLine("The file is invalid.");
                return;
            }

            byte[] salt = new byte[20];
            byte[] hash = new byte[40];
            
            Array.Copy(hashBytes, 0, salt, 0, salt.Length);
            Array.Copy(hashBytes, salt.Length, hash, 0, hash.Length);

            Console.WriteLine("Please type your password");
            string password = ReadPassword();

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] passHash = pbkdf2.GetBytes(40);

                string resultText = passHash.SequenceEqual(hash)
                    ? "Success! Password matches"
                    : "Sorry! Password doesn't match";

                Console.WriteLine(resultText);
            }
        }

        private static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                if (keyInfo.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += keyInfo.KeyChar;
                }
                else
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);

                        int pos = Console.CursorLeft;

                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                keyInfo = Console.ReadKey(true);
            }
            Console.WriteLine();
            return password;
        }
    }
}
