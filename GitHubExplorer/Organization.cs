using System.Text.Json.Serialization;

namespace GitHubExplorer {
    public class Organization {
        [JsonPropertyName("login")] public string Name { get; set; }
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("url")] public string Url { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
    }
}