using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitHubExplorer {
    class Program {
        static readonly HttpClient Client = new HttpClient();
        static string _userName;
        static string UserUri => $"https://api.github.com/users/{_userName}";

        static async Task Main(string[] args) {
            SetDefaultRequestHeaders();

            await GetUserName();

            bool validInput = false;
            bool browsing = true;
            int action = 0;

            while (browsing) {
                while (!validInput) {
                    Console.WriteLine("Choose Action:");
                    Console.WriteLine("0: Profile");
                    Console.WriteLine("1: Repositories");
                    Console.WriteLine("2: Organizations");
                    Console.WriteLine("3: Enter new username");
                    Console.WriteLine("4: Quit application");
                    const int availableActions = 5;
                    var userInput = Console.ReadLine();
                    validInput = int.TryParse(userInput, out int number) && number < availableActions;
                    action = number;
                }

                switch (action) {
                    case 0:
                        Console.WriteLine("Loading profile...\n");
                        await PrintUser();
                        break;
                    case 1:
                        Console.WriteLine("Loading repositories...\n");
                        await PrintRepositories();
                        break;
                    case 2:
                        Console.WriteLine("Loading organizations...\n");
                        await PrintOrganizations();
                        break;
                    case 3:
                        await GetUserName();
                        break;
                    case 4:
                        Console.WriteLine("\nQuitting application...\n");
                        browsing = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }

                Console.WriteLine("========================================\n");
                validInput = false;
            }
        }

        static async Task GetUserName() {
            while (true) {
                Console.WriteLine("Enter GitHub Username:");
                _userName = Console.ReadLine();
                if (_userName == "") {
                    Console.WriteLine("No username provided.\n");
                    break;
                }
                var user = await Find<User>(UserUri);
                if (user != null) {
                    Console.WriteLine("User found...\n");
                    break;
                }
                Console.WriteLine("Try again...\n");
            }
        }

        static void SetDefaultRequestHeaders() {
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            Client.DefaultRequestHeaders.Add("User-Agent", "GitHub Finder");
        }

        static async Task<T> Find<T>(string requestUri) {
            try {
                var streamTask = Client.GetStreamAsync(requestUri);
                return await JsonSerializer.DeserializeAsync<T>(await streamTask);
            }
            catch (HttpRequestException e) {
                Console.WriteLine($"User not found... \nError message: {e.Message}\n");
                return default;
            }
        }

        static async Task PrintUser() {
            var user = await Find<User>(UserUri);
            if (user == null)
                return;
            Console.WriteLine($"Username: {user.UserName}");
            Console.WriteLine($"User ID: {user.UserId}");
            Console.WriteLine($"Name: {user.Name}");
            Console.WriteLine($"Company: {user.Company}\n");
        }
        static async Task PrintOrganizations() {
            var orgs = await Find<List<Organization>>(UserUri + "/orgs");
            if (orgs == null)
                return;
            foreach (var org in orgs) {
                Console.WriteLine($"Name: {org.Name}");
                Console.WriteLine($"ID: {org.Id}");
                Console.WriteLine($"URL: {org.Url}");
                Console.WriteLine($"Description: {org.Description}\n");
            }
        }

        static async Task PrintRepositories() {
            var repos = await Find<List<Repository>>(UserUri + "/repos");
            if (repos == null) 
                return;;
            foreach (var repo in repos) {
                Console.WriteLine($"Name: {repo.Name}");
                Console.WriteLine($"Description: {repo.Description}");
                Console.WriteLine($"GitHubHomeUrl: {repo.GitHubHomeUrl}");
                Console.WriteLine($"HomePage: {repo.Homepage}");
                Console.WriteLine($"Watchers: {repo.Watchers}");
                Console.WriteLine($"Last Push: {repo.LastPush}\n");
            }
        }
    }
}
