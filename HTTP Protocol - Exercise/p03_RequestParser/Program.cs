namespace p03_RequestParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Program
    {
        private static readonly Dictionary<string, HashSet<string>> PathAndMethods =
            new Dictionary<string, HashSet<string>>();

        public static void Main()
        {
            string input = Console.ReadLine();

            while (input != "END")
            {
                string[] tokens = input.Split("/", StringSplitOptions.RemoveEmptyEntries);

                string path = tokens[0];

                string method = tokens[1];

                InitializePathAndMethods(path, method);

                input = Console.ReadLine();
            }

            string search = Console.ReadLine();

            string[] parts = search.Split(new[] {" ", "/"}, StringSplitOptions.RemoveEmptyEntries).Take(2)
                .ToArray();

            string methodToSearch = parts[0].ToLower();

            string pathToSearch = parts[1].ToLower();


            if (IsSearchValid(methodToSearch, pathToSearch))
            {
                string result = PrintValid();

                Console.WriteLine(result);
            }
            else
            {
                string result = PrintInvalid();

                Console.WriteLine(result);
            }
        }

        private static void InitializePathAndMethods(string path, string method)
        {
            if (!PathAndMethods.ContainsKey(path))
            {
                PathAndMethods[path] = new HashSet<string>();
            }

            PathAndMethods[path].Add(method);
        }

        private static string PrintInvalid()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"HTTP/1.1 {(int) StatusCode.NotFound} {StatusCode.NotFound.ToString()}")
                .AppendLine($"Content-Length: {StatusCode.NotFound.ToString().Length}")
                .AppendLine($"Content-Type: text/plain").AppendLine().AppendLine($"{StatusCode.NotFound.ToString()}");

            return sb.ToString().Trim();
        }

        private static string PrintValid()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"HTTP/1.1 {(int) StatusCode.OK} {StatusCode.OK.ToString()}")
                .AppendLine($"Content-Length: {StatusCode.OK.ToString().Length}")
                .AppendLine($"Content-Type: text/plain").AppendLine().AppendLine($"{StatusCode.OK.ToString()}");

            return sb.ToString().Trim();
        }

        private static bool IsSearchValid(string methodToSearch, string pathToSearch)
        {
            return PathAndMethods.ContainsKey(pathToSearch) &&
                   PathAndMethods[pathToSearch].Any(x => x == methodToSearch);
        }
    }
}