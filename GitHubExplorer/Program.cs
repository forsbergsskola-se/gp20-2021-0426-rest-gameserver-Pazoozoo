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
        static async Task Main(string[] args) {

            Console.WriteLine("Choose REST API:");
            Console.WriteLine("0: GitHub");
            // var userInput = int.Parse(Console.ReadLine());

            switch (0) {
                case 0:
                    Console.WriteLine("GitHub API");
                    await RunGitHubApi();
                    break;
                default:
                    Console.WriteLine("No API found");
                    break;
            }
        }

        static async Task RunGitHubApi() {
            var repos = await FetchRepositories();
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

            var streamTask = Client.GetStreamAsync("https://api.github.com/users/Pazoozoo/repos");
            var repos = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);
            
            return repos;
        }
    }
}
