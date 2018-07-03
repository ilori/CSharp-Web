namespace p01_UrlDecode
{
    using System;
    using System.Net;

    public class Program
    {
        public static void Main()
        {
            string input = Console.ReadLine();

            string result = WebUtility.UrlDecode(input);

            Console.WriteLine(result);
        }
    }
}