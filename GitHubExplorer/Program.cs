using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitHubExplorer {
    class Program {
        static readonly HttpClient Client = new HttpClient();
        static string _userName;
        static async Task Main(string[] args) {
            
            while (true) {
                Console.WriteLine("Enter GitHub UserID:");
                _userName = Console.ReadLine();
                if (_userName != null) break;
                Console.WriteLine("No input received.\n");
            }
            
            
            Console.WriteLine("Choose REST API:");
            Console.WriteLine("0: GitHub");
            // var userInput = int.Parse(Console.ReadLine());

            switch (0) {
                case 0:
                    Console.WriteLine("GitHub API");
                    await GitHubRepositories();
                    break;
                default:
                    Console.WriteLine("No API found");
                    break;
            }
        }

        static async Task GitHubRepositories() {
            var repos = await FetchRepositories();
            if (repos.Count == 0) 
                return;
            Console.WriteLine("Repos:\n");
            foreach (var repo in repos) {
                Console.WriteLine($"Name: {repo.Name}");
                Console.WriteLine($"Description: {repo.Description}");
                Console.WriteLine($"GitHubHomeUrl: {repo.GitHubHomeUrl}");
                Console.WriteLine($"HomePage: {repo.Homepage}");
                Console.WriteLine($"Watchers: {repo.Watchers}");
                Console.WriteLine($"Last Push: {repo.LastPush}\n");
            }
        }

        static async Task<List<Repository>> FetchRepositories() {
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            Client.DefaultRequestHeaders.Add("User-Agent", "GitHub Repository Finder");
            var repos = new List<Repository>();
            
            try {
                var streamTask = Client.GetStreamAsync($"https://api.github.com/users/{_userName}/repos");
                repos = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
            }
            catch (HttpRequestException e) {
                Console.WriteLine("User not found... Error message: " + e.Message);
            }
            return repos;
        }
    }
}
