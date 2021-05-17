using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter {
    public class RealTimeLameScooterRental : ILameScooterRental {
        List<LameScooterStationList> _scooterList;
        static readonly HttpClient client = new HttpClient();

        public async Task GetScooterDatabase(string uri) {
            Console.WriteLine($"Loading from: {uri}");
            List<LameScooterStationList> list = null;
            try {
                var options = new JsonSerializerOptions {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                list = await client.GetFromJsonAsync<List<LameScooterStationList>>(uri, options);
            }
            catch (HttpRequestException e) {
                Console.WriteLine($"Error: {e.Message}");
            }

            if (list == null) {
                Console.WriteLine("list is null");
            }
        }

        public Task<int> GetScooterCountInStation(string stationName) {
            if (stationName.Any(char.IsDigit)) 
                throw new ArgumentException($"{stationName} contains a digit.");

            var station = _scooterList.Find(list => list.Name == stationName);
            
            if (station != null) 
                return Task.FromResult(station.BikesAvailable);

            throw new NotFoundException(stationName);
        }
    }
}