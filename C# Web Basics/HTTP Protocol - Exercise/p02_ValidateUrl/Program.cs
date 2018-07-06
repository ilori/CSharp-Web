namespace p02_ValidateUrl
{
    using System;
    using System.Net;
    using System.Text;

    public class Program
    {
        public static void Main()
        {
            string input = Console.ReadLine();
            string url = WebUtility.UrlDecode(input);

            try
            {
                Uri parsedUrl = new Uri(url);

                if (!IsUrlValid(parsedUrl))
                {
                    throw new ArgumentException("Invalid URL");
                }

                StringBuilder builder = new StringBuilder();

                builder
                    .AppendLine($"Protocol: {parsedUrl.Scheme}")
                    .AppendLine($"Host: {parsedUrl.Host}")
                    .AppendLine($"Port: {parsedUrl.Port}")
                    .AppendLine($"Path: {parsedUrl.LocalPath}");

                if (!string.IsNullOrWhiteSpace(parsedUrl.Query))
                {
                    builder.AppendLine($"Query: {parsedUrl.Query.Substring(1)}");
                }

                if (!string.IsNullOrWhiteSpace(parsedUrl.Fragment))
                {
                    builder.AppendLine($"Fragment: {parsedUrl.Fragment.Substring(1)}");
                }

                Console.WriteLine(builder.ToString().Trim());
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid URL");
            }
        }

        private static bool IsUrlValid(Uri parsedUrl)
        {
            return string.IsNullOrWhiteSpace(parsedUrl.Scheme) ||
                   string.IsNullOrWhiteSpace(parsedUrl.Host) ||
                   string.IsNullOrWhiteSpace(parsedUrl.LocalPath) ||
                   !parsedUrl.IsDefaultPort;
        }
    }
}