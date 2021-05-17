using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter {
    public class RealTimeLameScooterRental : ILameScooterRental {
        ScooterStations _scooterStations;
        static readonly HttpClient client = new HttpClient();

        public async Task GetScooterDatabase(string uri) {
            Console.WriteLine($"Loading JSON data from: {uri}");
            
            try {
                var options = new JsonSerializerOptions {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var jsonString = await client.GetStringAsync(uri);
                _scooterStations = JsonSerializer.Deserialize<ScooterStations>(jsonString, options);
            }
            catch (HttpRequestException e) {
                Console.WriteLine($"Error message: {e.Message}");
            }
        }

        public Task<int> GetScooterCountInStation(string stationName) {
            if (stationName.Any(char.IsDigit)) 
                throw new ArgumentException($"{stationName} contains a digit.");

            if (_scooterStations == null) 
                throw new ArgumentException($"Could not find JSON data with provided uri.");
            
            var station = _scooterStations.Stations.Find(list => list.Name == stationName);
            
            if (station != null) 
                return Task.FromResult(station.BikesAvailable);

            throw new NotFoundException(stationName);
        }
    }
}