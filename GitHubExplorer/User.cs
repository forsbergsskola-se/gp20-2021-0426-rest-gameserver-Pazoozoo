using System.Text.Json.Serialization;

namespace GitHubExplorer {
    public class User {
        [JsonPropertyName("login")] public string UserName { get; set; }
        [JsonPropertyName("id")] public int UserId { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("company")] public string Company { get; set; }
    }
}