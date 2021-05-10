using System;
using System.Net.Http;

namespace GitHubExplorer {
    class Program {
        HttpClient Client = new HttpClient();
        static void Main(string[] args) {

            Console.WriteLine("Choose REST API:");
            Console.WriteLine("0: GitHub");
            var userInput = int.Parse(Console.ReadLine());

            switch (userInput) {
                case 0:
                    Console.WriteLine("GitHub API");
                    break;
                default:
                    Console.WriteLine("No API found");
                    break;
            }
        }
    }
}
